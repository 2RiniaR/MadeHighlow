using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class EventPerformerConnector : MonoBehaviour
    {
        private readonly List<IEventPerformer> _performers = new();

        public void Connect(IEventPerformer eventPerformer)
        {
            _performers.Add(eventPerformer);
            Debug.Log($"{name}: Event Performer が接続されました。", this);
        }

        public void Disconnect(IEventPerformer eventPerformer)
        {
            _performers.Remove(eventPerformer);
            Debug.Log($"{name}: Event Performer の接続が切断されました。", this);
        }

        public async UniTask PerformToLatest(CancellationToken token)
        {
            await UniTask.WhenAll(_performers.Select(player => player.PerformToLatest(token)));
        }
    }
}