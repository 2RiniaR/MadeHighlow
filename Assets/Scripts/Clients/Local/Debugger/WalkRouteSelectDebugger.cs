using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Field;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Debugger
{
    public class WalkRouteSelectDebugger : MonoBehaviour
    {
        public Figure walker;
        public bool activateOnStart;
        public WalkRouteSelector walkRouteSelector;

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

        [ContextMenu("Select Route")]
        private void Run()
        {
            Run(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask Run(CancellationToken token)
        {
            if (walker == null) Debug.LogError("Please set walker.");
            var window = Window.Current.OpenAsChild(walkRouteSelector.window).GetComponent<WalkRouteSelector>() ??
                         throw new NullReferenceException();
            var route = await window.SelectWalkRoute(walker, token);
            Debug.Log(route);
        }
    }
}