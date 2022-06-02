using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public class RegisterTileSimulator
    {
        public RegisterTileSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] RegisterTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RegisterTileResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Context.Modifier.CreateTile(Initial, Result.Registered);
        }
    }
}
