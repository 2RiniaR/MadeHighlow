using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     ゲームをプレイするプレイヤー。
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("Settings")]
        public League league;

        public Deck deck;

        [Header("References on scene")]
        public Session session;

        /// <summary>
        ///     現在接続されている、行動選択クライアント。
        /// </summary>
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

        /// <summary>
        ///     行動選択クライアントを接続する。
        /// </summary>
        /// <param name="client">接続するクライアント。</param>
        public void ConnectStrategySelector(IStrategySelector client)
        {
            if (_strategySelector != null)
            {
                DisconnectStrategySelector();
            }

            _strategySelector = client;
            this.LogInfo("行動選択クライアントが接続されました。");
        }

        /// <summary>
        ///     行動選択クライアントの接続を切断する。
        /// </summary>
        public void DisconnectStrategySelector()
        {
            if (_strategySelector == null)
            {
                return;
            }

            _strategySelector = null;
            this.LogInfo("行動選択クライアントの接続が切断されました。");
        }

        private void RefreshReferences()
        {
            session ??= GetComponentInParent<Session>();
        }

        /// <summary>
        ///     行動を選択する。
        /// </summary>
        public async UniTask SelectStrategy(CancellationToken token)
        {
            if (_strategySelector == null)
            {
                this.LogInfo("行動選択クライアントが存在しないため、行動選択がスキップされました。");
                return;
            }

            this.LogInfo("行動を選択しています...");
            await _strategySelector.SelectStrategy(token);
            this.LogInfo("行動選択が完了しました。");
        }
    }
}