using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    public abstract record EntityComponent
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID<EntityComponent> ID { get; init; } = ID<EntityComponent>.None;

        /// <summary>
        ///     有効期間
        /// </summary>
        [NotNull]
        public Duration Duration { get; init; } = Duration.Unlimited;

        public static EntityComponent Empty => new EmptyEntityComponent();

        private record EmptyEntityComponent : EntityComponent;
    }
}