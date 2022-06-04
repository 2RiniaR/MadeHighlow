using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
