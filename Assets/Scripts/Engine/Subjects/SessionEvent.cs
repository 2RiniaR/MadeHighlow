using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     セッション内で発生したイベント
    /// </summary>
    public record SessionEvent(in ID ID, [NotNull] in Result Result);
}