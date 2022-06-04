using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public class ReserveCommandSimulator
    {
        public ReserveCommandSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] ReserveCommandResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private ReserveCommandResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Initial with
                {
                    ReservedCommands = Initial.ReservedCommands.Add(succeedResult.Action.Command),
                };
            }

            return Initial;
        }
    }
}
