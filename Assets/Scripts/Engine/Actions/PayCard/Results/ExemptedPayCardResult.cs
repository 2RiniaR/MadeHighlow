using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードの支払いが免除された結果
    /// </summary>
    public record ExemptedPayCardResult([NotNull] ComponentID DecidedComponentID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
