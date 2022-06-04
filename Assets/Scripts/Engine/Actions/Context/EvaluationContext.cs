using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows;

namespace RineaR.MadeHighlow.Actions
{
    public class EvaluationContext : IEvaluationContext
    {
        public EvaluationContext(
            [NotNull] IActionRunner actions,
            [NotNull] IWorldFinder finder,
            [NotNull] IRandomGenerator randomGenerator,
            [NotNull] IEvaluationFlowProvider evaluationFlows
        )
        {
            Actions = actions;
            Finder = finder;
            RandomGenerator = randomGenerator;
            Flows = evaluationFlows;
        }

        public IActionRunner Actions { get; }
        public IWorldFinder Finder { get; }
        public IRandomGenerator RandomGenerator { get; }
        public IEvaluationFlowProvider Flows { get; }
    }
}
