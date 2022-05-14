using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus
    ) : Action<AddComponentResult>
    {
        protected override AddComponentResult EvaluateBody(IHistory context)
        {
            return new AddComponentEvaluator(context, TargetID, InitialStatus).Evaluate();
        }
    }
}
