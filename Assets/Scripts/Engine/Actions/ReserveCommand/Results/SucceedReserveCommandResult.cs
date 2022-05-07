using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedReserveCommandResult([NotNull] Command Command) : ReserveCommandResult
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
