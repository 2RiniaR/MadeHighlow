using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードの支払いが免除された結果
    /// </summary>
    public record ExemptedResult([NotNull] CardID CardID, [NotNull] ComponentID ExemptedComponentID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
