using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record TargetNotFoundResult([NotNull] KnockBackAction Action) : KnockBackResult;
}
