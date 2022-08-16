using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.GameModel
{
    public interface IEventPlayer
    {
        UniTask PlayEventsToLatest(CancellationToken token);
    }
}