using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.GameModel
{
    public interface IClient
    {
        UniTask SelectStrategy(Player submitter, CancellationToken token);
    }
}