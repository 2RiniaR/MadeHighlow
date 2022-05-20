using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record ExemptedResult(
        [NotNull] PayCardAction Action,
        [NotNull] PayCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts,
        [NotNull] ComponentID ExemptedID
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
