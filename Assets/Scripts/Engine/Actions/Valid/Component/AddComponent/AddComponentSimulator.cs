using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public class AddComponentSimulator
    {
        public AddComponentSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] AddComponentResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private AddComponentResult Result { get; }

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
