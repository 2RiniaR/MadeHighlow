using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public record DisallowedResult(
        [NotNull] Command Command,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandEffect>> Interrupts,
        [CanBeNull] ComponentID DisallowedID
    ) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
