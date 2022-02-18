using Cysharp.Threading.Tasks;
using Game.Entities;
using Game.Primitives;

namespace Game.Clients
{
    public class SampleClient : IClient
    {
        public ClientID ID { get; }

        public SampleClient(ClientID id)
        {
            ID = id;
        }

        public UniTask<IPlayerTurnAction> SubmitAction()
        {
            throw new System.NotImplementedException();
        }
    }
}