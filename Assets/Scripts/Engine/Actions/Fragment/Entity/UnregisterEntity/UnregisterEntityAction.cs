using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterEntity
{
    public record UnregisterEntityAction([NotNull] EntityID TargetID)
    {
        public UnregisterEntityResult Evaluate(IHistory history)
        {
            return new UnregisterEntityEvaluator(history, this).Evaluate();
        }
    }
}
