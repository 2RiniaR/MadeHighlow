using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityFly;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record EntityFlyFailedResult(
        [NotNull] KnockBackAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackCalculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] ReactedResult<EntityFlyResult> Failed
    ) : KnockBackResult;
}
