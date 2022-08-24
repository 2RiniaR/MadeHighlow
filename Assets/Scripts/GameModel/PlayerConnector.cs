using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class PlayerConnector : MonoBehaviour
    {
        [NonEditable]
        public List<Player> players;

        private readonly List<Player> _players = new();

        private void Start()
        {
            foreach (var player in GetComponentsInChildren<Player>())
            {
                Join(player);
            }
        }

        private void Update()
        {
            players = new List<Player>(_players);
        }

        private void OnDestroy()
        {
            foreach (var player in new List<Player>(_players))
            {
                Leave(player);
            }
        }

        /// <summary>
        ///     プレイヤーをゲームに参加させる。
        /// </summary>
        /// <param name="player">参加するプレイヤー。</param>
        public void Join(Player player)
        {
            _players.Add(player);
            this.LogInfo($"プレイヤー（{player.name}）がゲームに参加しました。");
        }

        /// <summary>
        ///     プレイヤーをゲームから離脱させる。
        /// </summary>
        /// <param name="player">離脱するプレイヤー。</param>
        public void Leave(Player player)
        {
            _players.Remove(player);
            this.LogInfo($"プレイヤー（{player.name}）がゲームから離脱しました。");
        }

        /// <summary>
        ///     全てのプレイヤーに、行動を選択させる。
        /// </summary>
        public async UniTask SelectStrategy(CancellationToken token)
        {
            this.LogInfo("すべてのプレイヤーが行動を選択するのを待っています...");
            await UniTask.WhenAll(_players.Select(player => player.SelectStrategy(token)));
            this.LogInfo("すべてのプレイヤーが行動の選択を完了しました。");
        }
    }
}