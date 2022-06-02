using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class CreatePlayerSimulator
    {
        public CreatePlayerSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] CreatePlayerResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private CreatePlayerResult Result { get; }

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
