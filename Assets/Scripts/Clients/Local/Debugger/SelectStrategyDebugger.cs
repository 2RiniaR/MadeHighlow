using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Strategy;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Debugger
{
    public class SelectStrategyDebugger : MonoBehaviour
    {
        [Header("Settings")]
        public bool activateOnStart;

        public StrategySelector strategySelector;

        [Header("References on scene")]
        public Player submitter;

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
                await UniTask.Yield(token);
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
            if (submitter == null)
            {
                Debug.LogError("Please set submitter.");
            }

            var window = Window.GetRoute().CreateChild(strategySelector.window);
            window.Open();

            var selector = window.GetComponent<StrategySelector>() ?? throw new NullReferenceException();
            await selector.SelectStrategy(token);
            if (token.IsCancellationRequested)
            {
                return;
            }

            Debug.Log("Submitted!");
        }
    }
}