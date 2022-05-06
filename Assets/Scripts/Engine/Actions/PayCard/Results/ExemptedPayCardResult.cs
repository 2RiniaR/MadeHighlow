using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードの支払いが免除された結果
    /// </summary>
    public record ExemptedPayCardResult : PayCardResult
    {
        /// <summary>
        ///     免除を決定したコンポーネントのID
        /// </summary>
        [NotNull]
        public ComponentID DecidedComponentID { get; init; } = new();

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}