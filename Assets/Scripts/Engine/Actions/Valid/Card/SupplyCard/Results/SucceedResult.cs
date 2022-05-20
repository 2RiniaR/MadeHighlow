using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record SucceedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] SupplyCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardEffect>> Interrupts
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
