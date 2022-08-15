using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Player : MonoBehaviour
    {
        public League league;
        public Deck deck;
        private IClient _client;
        public ISession Session { get; private set; }

        private void Start()
        {
            Session = GameModel.Session.ContextOf(this);
        }

        public void ConnectClient(IClient client)
        {
            _client = client;
        }

        public void DisconnectClient()
        {
            _client = null;
        }

        public async UniTask SelectStrategy(CancellationToken token)
        {
            _client.SelectStrategy(this, token);
        }

        public static Player OwnerOf(League league)
        {
            return league.GetComponentInParent<Player>();
        }

        public static Player OwnerOf(Deck deck)
        {
            return deck.GetComponentInParent<Player>();
        }
    }
}