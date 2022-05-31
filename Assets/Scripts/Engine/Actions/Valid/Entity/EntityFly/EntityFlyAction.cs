using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyAction
        ([NotNull] EntityID TargetID, [NotNull] EntityFlyRoute Route) : ValidAction<EntityFlyResult>
    {
        protected override EntityFlyResult EvaluateBody(IHistory history)
        {
            return new EntityFlyEvaluator(history, this).Evaluate();
        }
    }
}
