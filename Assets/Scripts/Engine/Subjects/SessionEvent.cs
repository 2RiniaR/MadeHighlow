using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     セッション内で発生したイベント
    /// </summary>
    public record SessionEvent
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = new();

        /// <summary>
        ///     アクションの結果
        /// </summary>
        [NotNull]
        public ISimulatable Result { get; init; } = ISimulatable.Empty;
    }
}