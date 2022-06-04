using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnSimulator
    {
        public IncrementTurnSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] IncrementTurnResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private IncrementTurnResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Initial with { CurrentTurn = Result.Updated };
        }
    }
}
