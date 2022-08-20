using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Debugger
{
    public class LoopGameDebugger : MonoBehaviour
    {
        [Header("Settings")]
        public bool activateOnStart;

        [Header("References on scene")]
        public Session session;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);
        }

        private void Start()
        {
            StartAsync(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            if (activateOnStart)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                Run();
            }
        }

        [ContextMenu("Start")]
        private void Run()
        {
            session.StartGame();
        }
    }
}