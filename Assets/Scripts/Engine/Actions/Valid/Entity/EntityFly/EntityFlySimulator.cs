using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public class EntityFlySimulator
    {
        public EntityFlySimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] EntityFlyResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private EntityFlyResult Result { get; }

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
