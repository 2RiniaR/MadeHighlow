using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record DisallowedResult(
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
