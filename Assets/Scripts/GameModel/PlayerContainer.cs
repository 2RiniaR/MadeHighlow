using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class PlayerContainer : MonoBehaviour
    {
        public List<Player> players;

        public async UniTask SelectStrategy(CancellationToken token)
        {
            await UniTask.WhenAll(players.Select(player => player.SelectStrategy(token)));
        }
    }
}