using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyAction
        ([NotNull] EntityID TargetID, [NotNull] ValueList<EntityFlyStep> Steps) : ValidAction<EntityFlyResult>
    {
        protected override EntityFlyResult EvaluateBody(IHistory history)
        {
            return new EntityFlyEvaluator(history, this).Evaluate();
        }
    }
}
