using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record EntityFlyFailedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> Calculations,
        [NotNull] KnockBack Calculated,
        [NotNull] ReactedResult<EntityFly.Result> Failed
    ) : Result;
}
