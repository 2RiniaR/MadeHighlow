using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record SucceedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] Process Process
    ) : Result;
}
