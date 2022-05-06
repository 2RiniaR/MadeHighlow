using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record EntityVitality
    {
        /// <summary>
        ///     体力
        /// </summary>
        [NotNull]
        public EntityHealth Health { get; init; } = new();

        /// <summary>
        ///     最大体力
        /// </summary>
        [NotNull]
        public EntityHealth MaxHealth { get; init; } = new();

        public bool IsDead => Health.Value <= MaxHealth.Value;

        public EntityVitality Dead => this with { Health = new EntityHealth() };
    }
}