using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public class UnregisterEntitySimulator
    {
        public UnregisterEntitySimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] UnregisterEntityResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private UnregisterEntityResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.DeleteEntity(Initial, succeedResult.Action.TargetID);
            }

            return Initial;
        }
    }
}
