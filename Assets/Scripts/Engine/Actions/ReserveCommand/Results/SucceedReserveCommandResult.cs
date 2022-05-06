using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedReserveCommandResult<TOption>([NotNull] in Command<TOption> Command) : ReserveCommandResult
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