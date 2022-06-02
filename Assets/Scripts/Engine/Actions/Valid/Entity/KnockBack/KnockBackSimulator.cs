using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public class KnockBackSimulator
    {
        public KnockBackSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] KnockBackResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private KnockBackResult Result { get; }

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
