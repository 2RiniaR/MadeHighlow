using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public class PositionEntitySimulator
    {
        public PositionEntitySimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] PositionEntityResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private PositionEntityResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.UpdateEntity(Initial, succeedResult.Positioned);
            }

            return Initial;
        }
    }
}
