using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyAction([NotNull] EntityID TargetID, [NotNull] EntityFlyRoute Route) : IValidAction;
}
