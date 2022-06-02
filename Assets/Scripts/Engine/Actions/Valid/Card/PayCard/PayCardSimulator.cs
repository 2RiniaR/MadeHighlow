using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public class PayCardSimulator
    {
        public PayCardSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] PayCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private PayCardResult Result { get; }

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
