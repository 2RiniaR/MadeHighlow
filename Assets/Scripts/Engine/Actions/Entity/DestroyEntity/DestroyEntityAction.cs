using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : Action<DestroyEntityResult>
    {
        protected override DestroyEntityResult EvaluateBody(IActionContext context)
        {
            return new DestroyEntityEvaluator(context, TargetID).Evaluate();
        }
    }
}
