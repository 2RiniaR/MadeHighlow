using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.GameModel
{
    public interface IStrategySelector
    {
        UniTask SelectStrategy(Player submitter, CancellationToken token);
    }
}