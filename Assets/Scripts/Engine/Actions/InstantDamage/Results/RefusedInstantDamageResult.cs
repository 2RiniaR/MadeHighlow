using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージが無効化された結果
    /// </summary>
    public record RefusedInstantDamageResult([NotNull] in ComponentID DecidedComponentID) : InstantDamageResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}