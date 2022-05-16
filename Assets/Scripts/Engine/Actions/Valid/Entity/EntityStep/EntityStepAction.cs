using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record EntityStepAction(
        [NotNull] EntityID TargetID,
        [NotNull] Direction2D Direction,
        [NotNull] EntityStepCost Available
    ) : ValidAction<EntityStepResult>
    {
        protected override EntityStepResult EvaluateBody(IHistory history)
        {
            return new EntityStepEvaluator(history, this).Evaluate();
        }
    }
}
