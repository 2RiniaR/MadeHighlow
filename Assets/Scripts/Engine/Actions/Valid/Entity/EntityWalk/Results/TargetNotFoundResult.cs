using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record TargetNotFoundResult([NotNull] EntityWalkAction Action) : EntityWalkResult;
}
