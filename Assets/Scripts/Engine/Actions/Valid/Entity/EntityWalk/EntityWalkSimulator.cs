using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public class EntityWalkSimulator
    {
        public EntityWalkSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] EntityWalkResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private EntityWalkResult Result { get; }

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
