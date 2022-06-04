using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt> Rejections,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
