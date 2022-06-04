using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
