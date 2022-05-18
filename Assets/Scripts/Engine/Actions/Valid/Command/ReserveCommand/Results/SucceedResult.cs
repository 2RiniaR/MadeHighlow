using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public record SucceedResult(
        [NotNull] ReserveCommandAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandEffect>> Interrupts,
        [NotNull] ComponentID AllowedID
    ) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world with
            {
                ReservedCommands = world.ReservedCommands.Add(Action.Command),
            };
        }
    }
}
