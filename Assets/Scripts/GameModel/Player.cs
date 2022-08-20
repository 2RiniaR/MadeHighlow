using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Player : MonoBehaviour
    {
        public League league;
        public Deck deck;
        public Session session;
        private IStrategySelector _strategySelector;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void OnDestroy()
        {
            DisconnectStrategySelector();
        }

        public void ConnectStrategySelector(IStrategySelector client)
        {
            if (_strategySelector != null)
            {
                DisconnectStrategySelector();
            }

            _strategySelector = client;
            Debug.Log($"{name}: Strategy Selector が接続されました。", this);
        }

        public void DisconnectStrategySelector()
        {
            if (_strategySelector == null)
            {
                return;
            }

            _strategySelector = null;
            Debug.Log($"{name}: Strategy Selector の接続が切断されました。", this);
        }

        private void RefreshReferences()
        {
            session ??= GetComponentInParent<Session>();
        }

        public async UniTask SelectStrategy(CancellationToken token)
        {
            if (_strategySelector == null)
            {
                return;
            }

            await _strategySelector.SelectStrategy(token);
        }
    }
}