using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果が無効化された結果
    /// </summary>
    public record RefusedInstantHealResult([NotNull] ComponentID DecidedComponentID) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
