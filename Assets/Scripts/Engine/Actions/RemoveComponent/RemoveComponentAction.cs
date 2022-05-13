using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : Action<RemoveComponentResult>
    {
        public override RemoveComponentResult Evaluate(IActionContext context)
        {
            return new RemoveComponentEvaluator(context, TargetID).Evaluate();
        }
    }
}
