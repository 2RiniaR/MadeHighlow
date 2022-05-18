using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record RejectedResult(
        [NotNull] PayCardAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
