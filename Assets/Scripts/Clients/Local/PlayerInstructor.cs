using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Strategy;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local
{
    public class PlayerInstructor : MonoBehaviour, IStrategySelector
    {
        [Header("Settings")]
        public StrategySelector strategySelector;

        [Header("References on scene")]
        public Player player;

        private void Start()
        {
            player.ConnectStrategySelector(this);
        }

        private void OnDestroy()
        {
            if (player != null)
            {
                player.DisconnectStrategySelector();
            }
        }

        public async UniTask SelectStrategy(CancellationToken token)
        {
            var window = Window.GetRoute().CreateChild(strategySelector.window);
            window.Open();

            var instance = window.GetComponent<StrategySelector>() ?? throw new NullReferenceException();
            instance.player = player;
            await instance.SelectStrategy(token);
            if (token.IsCancellationRequested)
            {
                return;
            }

            window.Close();
            window.Dispose();
        }
    }
}