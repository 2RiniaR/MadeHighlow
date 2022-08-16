using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Session : MonoBehaviour
    {
        public int turn;
        public Field field;
        public CommandStack commandStack;
        public EventRunner eventRunner;
        public PlayerContainer players;
        public ClientContainer clients;
        private readonly List<Player> _ranking = new();
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);
        }

        private void Start()
        {
            turn = 0;
            _ranking.Clear();
            Run(_cancellationTokenSource.Token).Forget();
        }

        public async UniTask Run(CancellationToken token)
        {
            while (true)
            {
                await players.SelectStrategy(token);

                eventRunner.RunCommandsByOrder(commandStack.ReservedCommands);
                eventRunner.UpdateTurn();

                await clients.PlayEventsToLatest(token);

                if (IsGameSet()) break;
            }
        }

        private bool IsGameSet()
        {
            return _ranking.Count == players.players.Count;
        }
    }
}