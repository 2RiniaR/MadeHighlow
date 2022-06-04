using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public class BigBangSimulator
    {
        public BigBangSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] BigBangResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private BigBangResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Result.Process.Timeline.Simulate(Context, Initial);
        }
    }
}
