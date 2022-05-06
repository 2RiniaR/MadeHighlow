using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードの支払いが免除された結果
    /// </summary>
    public record ExemptedPayCardResult([NotNull] in ComponentID DecidedComponentID) : PayCardResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}