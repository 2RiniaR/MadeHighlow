using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : ValidAction<DestroyEntityResult>
    {
        protected override DestroyEntityResult EvaluateBody(IHistory history)
        {
            return new DestroyEntityEvaluator(history, TargetID).Evaluate();
        }
    }
}
