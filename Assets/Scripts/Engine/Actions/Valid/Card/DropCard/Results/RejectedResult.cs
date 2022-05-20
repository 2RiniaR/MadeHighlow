using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record RejectedResult(
        [NotNull] DropCardAction Action,
        [NotNull] DropCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
