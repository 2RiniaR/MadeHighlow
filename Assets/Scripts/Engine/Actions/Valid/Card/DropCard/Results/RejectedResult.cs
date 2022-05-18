using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record RejectedResult(
        [NotNull] DropCardAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
