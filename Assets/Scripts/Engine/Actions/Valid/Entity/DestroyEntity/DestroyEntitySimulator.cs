using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public class DestroyEntitySimulator
    {
        public DestroyEntitySimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] DestroyEntityResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private DestroyEntityResult Result { get; }

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
