using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record AllowedReserveCommandResult
        ([NotNull] Command Command, [NotNull] ComponentID AllowedComponentID) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world with
            {
                ReservedCommands = world.ReservedCommands.Add(Command),
            };
        }
    }
}
