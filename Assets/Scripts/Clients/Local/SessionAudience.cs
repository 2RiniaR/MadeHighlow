using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Performers;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local
{
    public class SessionAudience : MonoBehaviour, IEventPerformer
    {
        [Header("Settings")]
        public EventPerformer eventPerformer;

        [Header("References on scene")]
        public Player player;

        private void Start()
        {
            player.session.eventPerformerConnector.Connect(this);
        }

        private void OnDestroy()
        {
            player.session.eventPerformerConnector.Disconnect(this);
        }

        public async UniTask PerformToLatest(CancellationToken token)
        {
            if (eventPerformer == null)
            {
                return;
            }

            var window = Window.GetRoute().CreateChild(eventPerformer.window);
            window.Open();

            var instance = window.GetComponent<EventPerformer>() ?? throw new NullReferenceException();
            instance.logger = player.session.eventLogger;
            await instance.PerformToLatest(token);
            if (token.IsCancellationRequested)
            {
                return;
            }

            window.Close();
            window.Dispose();
        }
    }
}