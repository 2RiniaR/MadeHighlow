using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public class BigBangSimulator
    {
        public BigBangSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] BigBangResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private BigBangResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Result.Process.Timeline.Simulate(Context, Initial);
        }
    }
}
