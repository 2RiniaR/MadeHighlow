using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DisallowedReserveCommandResult(
        [NotNull] Command Command,
        [CanBeNull] ComponentID DisallowedComponentID
    ) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
