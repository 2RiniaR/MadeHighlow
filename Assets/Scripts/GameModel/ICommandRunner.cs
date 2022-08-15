using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.GameModel
{
    public interface ICommandRunner
    {
        UniTask Run(CancellationToken token);
    }
}