using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージが無効化された結果
    /// </summary>
    public record RefusedInstantDamageResult : InstantDamageResult
    {
        /// <summary>
        ///     無効化を決定したコンポーネントのID
        /// </summary>
        [NotNull]
        public ComponentEnsuredID DecidedComponentID { get; init; } = new();

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}