using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Primitives;

namespace Game.Entities
{
    public interface IClient
    {
        public ClientID ID { get; }
        public UniTask<IPlayerTurnAction> SubmitAction(CancellationToken cancellationToken = new CancellationToken());
    }
}