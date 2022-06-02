using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public class UnregisterCardSimulator
    {
        public UnregisterCardSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] UnregisterCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private UnregisterCardResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.DeleteCard(Initial, succeedResult.Action.TargetID);
            }

            return Initial;
        }
    }
}
