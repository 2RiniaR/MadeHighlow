using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    public abstract record TileComponent
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID<TileComponent> ID { get; init; } = ID<TileComponent>.None;

        /// <summary>
        ///     有効期間
        /// </summary>
        [NotNull]
        public Duration Duration { get; init; } = Duration.Unlimited;

        public static TileComponent Empty => new EmptyTileComponent();

        private record EmptyTileComponent : TileComponent;
    }
}