using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityVitality
    {
        /// <summary>
        ///     体力
        /// </summary>
        [NotNull]
        public UnitHealth Health { get; init; } = new();

        /// <summary>
        ///     最大体力
        /// </summary>
        [NotNull]
        public UnitHealth MaxHealth { get; init; } = new();

        public bool IsDead => Health.Value <= MaxHealth.Value;
    }
}