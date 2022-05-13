using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus
    ) : Action<AddComponentResult>
    {
        public override AddComponentResult Evaluate(IActionContext context)
        {
            return new ActionEvaluator(context, TargetID, InitialStatus).Evaluate();
        }
    }
}
