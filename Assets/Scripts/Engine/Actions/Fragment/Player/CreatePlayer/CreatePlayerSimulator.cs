using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class CreatePlayerSimulator
    {
        public CreatePlayerSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] CreatePlayerResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
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
