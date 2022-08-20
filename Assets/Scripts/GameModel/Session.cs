using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Session : MonoBehaviour
    {
        [Header("Settings")]
        public Field field;

        public CommandStack commandStack;
        public EventProcessor eventProcessor;
        public EventPerformerConnector eventPerformerConnector;
        public PlayerConnector playerConnector;
        public EventLogger eventLogger;

        [Header("States")]
        public int turn;

        private CancellationTokenSource _cancellationTokenSource;

        public void StartGame()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);
            LoopAsync(_cancellationTokenSource.Token).Forget();
        }

        public async UniTask LoopAsync(CancellationToken token)
        {
            turn = 0;

            while (true)
            {
                turn += 1;
                Debug.Log($"{name}: ターン {turn} が開始しました。", this);

                await playerConnector.SelectStrategy(token);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                eventProcessor.RunCommandsByOrder(commandStack.ReservedCommands);
                eventProcessor.UpdateTurn();

                await eventPerformerConnector.PerformToLatest(token);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await UniTask.Yield(token);
            }
        }

        public void StopGame()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}