using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus
    ) : Action<AddComponentResult>
    {
        protected override AddComponentResult EvaluateBody(IHistory history)
        {
            return new AddComponentEvaluator(history, TargetID, InitialStatus).Evaluate();
        }
    }
}
