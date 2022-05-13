using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : Action<DestroyEntityResult>
    {
        public override DestroyEntityResult Evaluate(IActionContext context)
        {
            return new DestroyEntityEvaluator(context, TargetID).Evaluate();
        }
    }
}
