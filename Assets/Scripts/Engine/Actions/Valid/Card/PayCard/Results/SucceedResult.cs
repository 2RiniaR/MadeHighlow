using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record SucceedResult(
        [NotNull] PayCardAction Action,
        [NotNull] PayCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardExemption>> ExemptionInterrupts
    ) : PayCardResult;
}
