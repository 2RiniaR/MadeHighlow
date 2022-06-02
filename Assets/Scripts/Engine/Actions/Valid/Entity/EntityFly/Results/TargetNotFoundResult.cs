using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record TargetNotFoundResult([NotNull] EntityFlyAction Action) : EntityFlyResult;
}
