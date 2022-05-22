using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record DisallowedResult(
        [NotNull] ReserveCommandAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandAcceptance>> AcceptanceInterrupts,
        [CanBeNull] ComponentID DisallowedID
    ) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
