using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record RejectedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
