using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnEvaluator
    {
        public IncrementTurnEvaluator([NotNull] IHistory initial)
        {
            Initial = initial;
        }

        [NotNull] private IHistory Initial { get; }

        public IncrementTurnResult Evaluate()
        {
            return new IncrementTurnResult(Initial.World.CurrentTurn.Increment());
        }
    }
}
