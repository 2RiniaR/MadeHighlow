using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public class Simulator
    {
        public Simulator([NotNull] ISimulationContext context, [NotNull] World initial, [NotNull] Result result)
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private Result Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result.Deleted == null) return Initial;
            return Context.Modifier.DeleteCard(Initial, Result.Deleted);
        }
    }
}
