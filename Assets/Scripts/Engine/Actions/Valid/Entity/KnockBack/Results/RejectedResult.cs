using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record RejectedResult(
        [NotNull] KnockBackAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackCalculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] KnockBackProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackRejection>> Rejections,
        [NotNull] ComponentID RejectedID
    ) : KnockBackResult;
}
