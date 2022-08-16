using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Player : MonoBehaviour
    {
        public League league;
        public Deck deck;
        public Client client;
        public Session session;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void RefreshReferences()
        {
            session ??= GetComponentInParent<Session>();
        }

        public UniTask SelectStrategy(CancellationToken token)
        {
            return client.SelectStrategy(this, token);
        }
    }
}