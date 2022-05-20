using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record EntityFlyAction
        ([NotNull] EntityID TargetID, [NotNull] Direction3D Direction) : ValidAction<EntityFlyResult>
    {
        protected override EntityFlyResult EvaluateBody(IHistory history)
        {
            return new EntityFlyEvaluator(history, this).Evaluate();
        }
    }
}
