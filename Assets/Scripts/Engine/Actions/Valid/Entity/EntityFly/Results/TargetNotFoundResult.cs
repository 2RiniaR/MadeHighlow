using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
