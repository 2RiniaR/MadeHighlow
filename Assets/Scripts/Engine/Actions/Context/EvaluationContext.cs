using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public class EvaluationContext : IEvaluationContext
    {
        public EvaluationContext(
            [NotNull] IActionRunner actions,
            [NotNull] IWorldFinder finder,
            [NotNull] IRandomGenerator randomGenerator
        )
        {
            Actions = actions;
            Finder = finder;
            RandomGenerator = randomGenerator;
        }

        public IActionRunner Actions { get; }
        public IWorldFinder Finder { get; }
        public IRandomGenerator RandomGenerator { get; }
    }
}
