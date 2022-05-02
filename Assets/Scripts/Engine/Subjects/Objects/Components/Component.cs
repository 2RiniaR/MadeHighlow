using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    /// <param name="Type">種類</param>
    public abstract record Component([NotNull] in ComponentType Type)
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID<Component> ID { get; init; } = ID<Component>.None;

        /// <summary>
        ///     有効期間
        /// </summary>
        [NotNull]
        public Duration Duration { get; init; } = Duration.Unlimited;

        /// <summary>
        ///     種類
        /// </summary>
        [NotNull]
        public ComponentType Type { get; } = Type;
    }
}