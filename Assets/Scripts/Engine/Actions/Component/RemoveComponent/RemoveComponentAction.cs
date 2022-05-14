using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : Action<RemoveComponentResult>
    {
        protected override RemoveComponentResult EvaluateBody(IActionContext context)
        {
            return new RemoveComponentEvaluator(context, TargetID).Evaluate();
        }
    }
}
