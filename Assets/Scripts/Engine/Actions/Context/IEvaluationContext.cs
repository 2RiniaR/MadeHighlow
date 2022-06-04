using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows;

namespace RineaR.MadeHighlow.Actions
{
    public interface IEvaluationContext
    {
        [NotNull] IActionRunner Actions { get; }
        [NotNull] IWorldFinder Finder { get; }
        [NotNull] IRandomGenerator RandomGenerator { get; }
        [NotNull] IEvaluationFlowProvider Flows { get; }
    }
}
