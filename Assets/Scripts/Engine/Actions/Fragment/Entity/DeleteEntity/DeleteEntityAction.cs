using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record DeleteEntityAction([NotNull] EntityID TargetID)
    {
        public DeleteEntityResult Evaluate(IHistory history)
        {
            return new DeleteEntityEvaluator(history, this).Evaluate();
        }
    }
}
