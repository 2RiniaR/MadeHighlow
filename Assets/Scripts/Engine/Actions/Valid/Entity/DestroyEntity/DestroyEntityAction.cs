using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : ValidAction<DestroyEntityResult>
    {
        protected override DestroyEntityResult EvaluateBody(IHistory history)
        {
            return new DestroyEntityEvaluator(history, this).Evaluate();
        }
    }
}
