using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public record CreateComponentAction([NotNull] IAttachableID TargetID, [NotNull] Component InitialStatus)
    {
        [NotNull]
        public CreateComponentResult Evaluate([NotNull] IHistory history)
        {
            return new CreateComponentEvaluator(history, this).Evaluate();
        }
    }
}
