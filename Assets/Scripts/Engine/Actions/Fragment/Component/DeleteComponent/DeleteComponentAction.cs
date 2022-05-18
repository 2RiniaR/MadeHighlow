using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public record DeleteComponentAction([NotNull] ComponentID TargetID)
    {
        [NotNull]
        public DeleteComponentResult Evaluate([NotNull] IHistory history)
        {
            return new DeleteComponentEvaluator(history, this).Evaluate();
        }
    }
}
