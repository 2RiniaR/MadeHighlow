using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record SucceedResult(
        [NotNull] Command Command,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandEffect>> Interrupts,
        [NotNull] ComponentID AllowedID
    ) : ReserveCommandResult
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
