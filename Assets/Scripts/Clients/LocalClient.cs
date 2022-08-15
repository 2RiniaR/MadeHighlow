using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local;
using RineaR.MadeHighlow.Clients.Local.Strategy;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients
{
    [RequireComponent(typeof(Window))]
    public class LocalClient : MonoBehaviour
    {
        public StrategySelector strategySelector;
        public Window Window { get; private set; }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        public UniTask SelectStrategy(Player submitter, CancellationToken token)
        {
            var window = Window.OpenAsChild(strategySelector.window).GetComponent<StrategySelector>() ??
                         throw new NullReferenceException();
            return window.SelectStrategy(submitter, token);
        }

        private void RefreshReferences()
        {
            Window = GetComponent<Window>() ?? throw new NullReferenceException();
        }
    }
}