using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CanceledRunCommandResult(
        [NotNull] Command Command,
        [NotNull] ComponentID CanceledComponentID
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
