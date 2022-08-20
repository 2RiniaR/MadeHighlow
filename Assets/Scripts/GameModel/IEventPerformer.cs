using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.GameModel
{
    public interface IEventPerformer
    {
        UniTask PerformToLatest(CancellationToken token);
    }
}