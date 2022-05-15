using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : ValidAction<RemoveComponentResult>
    {
        protected override RemoveComponentResult EvaluateBody(IHistory history)
        {
            return new RemoveComponentEvaluator(history, TargetID).Evaluate();
        }
    }
}
