using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Strategy;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace RineaR.MadeHighlow.Clients.Local.Debugger
{
    public class SelectStrategyDebugger : MonoBehaviour
    {
        public Player submitter;
        public bool activateOnStart;

        [FormerlySerializedAs("strategyWindow")]
        public StrategySelector strategySelector;

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
                await Run(token);
            }
        }

        [ContextMenu("Submit Actions")]
        private void Run()
        {
            Run(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask Run(CancellationToken token)
        {
            if (submitter == null) Debug.LogError("Please set submitter.");
            var window = Window.Current.OpenAsChild(strategySelector.window).GetComponent<StrategySelector>() ??
                         throw new NullReferenceException();
            await window.SelectStrategy(submitter, token);
            Debug.Log("Submitted!");
        }
    }
}