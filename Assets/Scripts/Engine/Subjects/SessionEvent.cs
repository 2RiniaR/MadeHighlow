using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     セッション内で発生したイベント
    /// </summary>
    public record SessionEvent(ID ID, [NotNull] Result Result);
}
