namespace RineaR.MadeHighlow.Actions
{
    public class EvaluationContext
    {
        public EvaluationContext(IActionRunner actions, IWorldFinder finder, IRandomGenerator randomGenerator)
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
