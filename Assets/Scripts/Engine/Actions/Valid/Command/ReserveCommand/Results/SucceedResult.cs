using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record SucceedResult(
        [NotNull] ReserveCommandAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandAcceptance>> AcceptanceInterrupts,
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
