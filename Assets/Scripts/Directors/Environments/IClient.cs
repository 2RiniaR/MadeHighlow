using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.MadeHighlow.Directors.Environments
{
    /// <summary>
    ///     「クライアント」を表現する
    /// </summary>
    public interface IClient
    {
        /// <summary>
        ///     次のターンの行動を決定する
        /// </summary>
        public UniTask<PlayerSubmission> Operate(CancellationToken cancellationToken);
    }
}
