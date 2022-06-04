using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public class DropCardSimulator
    {
        public DropCardSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] DropCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private DropCardResult Result { get; }

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
