using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class EventPerformerConnector : MonoBehaviour
    {
        private readonly List<IEventPerformer> _performers = new();

        private void OnDestroy()
        {
            foreach (var performer in new List<IEventPerformer>(_performers))
            {
                Disconnect(performer);
            }
        }

        public void Connect(IEventPerformer eventPerformer)
        {
            _performers.Add(eventPerformer);
            this.LogInfo("演出クライアントが接続されました。");
        }

        public void Disconnect(IEventPerformer eventPerformer)
        {
            _performers.Remove(eventPerformer);
            this.LogInfo("演出クライアントの接続が切断されました。");
        }

        public async UniTask PerformToLatest(CancellationToken token)
        {
            await UniTask.WhenAll(_performers.Select(player => player.PerformToLatest(token)));
        }
    }
}