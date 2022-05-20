using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record ExemptedResult(
        [NotNull] PayCardAction Action,
        [NotNull] PayCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardExemption>> ExemptionInterrupts,
        [NotNull] ComponentID ExemptedID
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
