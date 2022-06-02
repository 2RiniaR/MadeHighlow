using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record SucceedResult(
        [NotNull] KnockBackAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackCalculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] KnockBackProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackRejection>> Rejections
    ) : KnockBackResult;
}
