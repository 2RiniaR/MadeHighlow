using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IEvaluationContext
    {
        [NotNull] IActionRunner Actions { get; }
        [NotNull] IWorldFinder Finder { get; }
        [NotNull] IRandomGenerator RandomGenerator { get; }
    }
}
