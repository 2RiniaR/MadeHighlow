using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.GameModel.Interfaces;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Session : MonoBehaviour, ISession
    {
        public int turn;
        public Field field;
        public CommandStack commandStack;
        public PlayerContainer players;
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

        public int Turn => turn;
        public Field Field => field;
        public CommandStack CommandStack => commandStack;
        public PlayerContainer Players => players;

        public async UniTask Run(CancellationToken token)
        {
            while (true)
            {
                await Players.DetermineCommands(token);
                await CommandStack.RunAll(token);
                UpdateTurn();
                Judge();
                if (IsGameSet()) break;
            }
        }

        public void UpdateTurn()
        {
            var updaters = field.GetComponentsInChildren<ITurnUpdater>().ToList();
            updaters.Sort((item1, item2) => item1.UpdateTurnPriority.CompareTo(item2.UpdateTurnPriority));
            foreach (var updater in updaters) updater.UpdateTurn();
        }

        private bool IsGameSet()
        {
            return _ranking.Count == Players.players.Count;
        }

        public void Judge()
        {
        }

        public static Session ContextOf(Component component)
        {
            return component.GetComponentInParent<Session>();
        }
    }
}