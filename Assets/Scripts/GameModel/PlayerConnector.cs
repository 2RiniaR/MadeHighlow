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

        public void Join(Player player)
        {
            _players.Add(player);
            Debug.Log($"{name}: Player（{player.name}）が接続されました。", this);
        }

        public void Leave(Player player)
        {
            _players.Remove(player);
            Debug.Log($"{name}: Player（{player.name}）の接続が切断されました。", this);
        }

        public async UniTask SelectStrategy(CancellationToken token)
        {
            await UniTask.WhenAll(_players.Select(player => player.SelectStrategy(token)));
        }
    }
}