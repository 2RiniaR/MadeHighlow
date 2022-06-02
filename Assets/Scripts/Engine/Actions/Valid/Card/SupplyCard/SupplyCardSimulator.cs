using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public class SupplyCardSimulator
    {
        public SupplyCardSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] SupplyCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private SupplyCardResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return succeedResult.Process.Timeline.Simulate(Context, Initial);
            }

            return Initial;
        }
    }
}
