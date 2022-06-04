using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public class UpdateTurnSimulator
    {
        public UpdateTurnSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] UpdateTurnResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private UpdateTurnResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Result.Process.Timeline.Simulate(Context, Initial);
        }
    }
}
