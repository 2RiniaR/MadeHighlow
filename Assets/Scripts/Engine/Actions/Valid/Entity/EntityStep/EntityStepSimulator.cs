using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public class EntityStepSimulator
    {
        public EntityStepSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] EntityStepResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private EntityStepResult Result { get; }

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
