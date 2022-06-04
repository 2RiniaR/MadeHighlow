using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public class UnregisterTileSimulator
    {
        public UnregisterTileSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] UnregisterTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private UnregisterTileResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.DeleteTile(Initial, succeedResult.Action.TargetID);
            }

            return Initial;
        }
    }
}
