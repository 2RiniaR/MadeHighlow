using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     ゲームの進行。ここを中心にゲームが進行していく。
    /// </summary>
    public class Session : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("ゲームが行われるフィールド。")]
        public Field field;

        public CommandStack commandStack;
        public EventProcessor eventProcessor;
        public EventPerformerConnector eventPerformerConnector;
        public PlayerConnector playerConnector;
        public EventLogger eventLogger;

        [Header("States")]
        [Tooltip("現在のターン数。")]
        [Min(0)]
        public int turn;

        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     ゲームを開始する。
        /// </summary>
        public void StartGame()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);
            LoopAsync(_cancellationTokenSource.Token).Forget();
        }

        private void InitializeGame()
        {
            turn = 0;
            this.LogInfo("ゲームの初期化が完了しました。");
        }

        public async UniTask LoopAsync(CancellationToken token)
        {
            InitializeGame();

            this.LogInfo("ゲームが開始されました。");
            while (true)
            {
                turn += 1;
                this.LogInfo($"ターン {turn} が開始しました。");

                await playerConnector.SelectStrategy(token);
                if (token.IsCancellationRequested)
                {
                    this.LogInfo("ゲームが中断されました。");
                    return;
                }

                eventProcessor.RunCommandsByOrder(commandStack.ReservedCommands);
                eventProcessor.UpdateTurn();

                await eventPerformerConnector.PerformToLatest(token);
                if (token.IsCancellationRequested)
                {
                    this.LogInfo("ゲームが中断されました。");
                    return;
                }

                // 万が一の無限ループを防止するため、ループの最後で1フレーム待機する。
                await UniTask.Yield(token);
            }
        }

        /// <summary>
        ///     ゲームを中断する。
        /// </summary>
        public void StopGame()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}