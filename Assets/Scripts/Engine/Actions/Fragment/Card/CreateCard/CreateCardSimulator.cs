using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public class CreateCardSimulator
    {
        public CreateCardSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] CreateCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private CreateCardResult Result { get; }

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
