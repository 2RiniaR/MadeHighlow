using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDSimulator
    {
        public AllocateIDSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] AllocateIDResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private AllocateIDResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Initial with { LatestAllocatedID = Result.AllocatedID };
        }
    }
}
