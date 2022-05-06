using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedReserveCommandResult([NotNull] in Command Command) : ReserveCommandResult
    {
        public override World Simulate(in World world)
        {
            return world with
            {
                ReservedCommands = world.ReservedCommands.Add(Command),
            };
        }
    }
}