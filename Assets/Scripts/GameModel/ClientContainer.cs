using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class ClientContainer : MonoBehaviour
    {
        public List<Client> clients;

        public async UniTask PlayEventsToLatest(CancellationToken token)
        {
            await UniTask.WhenAll(clients.Select(client => client.PlayEventsToLatest(token)));
        }
    }
}