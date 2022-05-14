using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : Action<RemoveComponentResult>
    {
        protected override RemoveComponentResult EvaluateBody(IHistory history)
        {
            return new RemoveComponentEvaluator(history, TargetID).Evaluate();
        }
    }
}
