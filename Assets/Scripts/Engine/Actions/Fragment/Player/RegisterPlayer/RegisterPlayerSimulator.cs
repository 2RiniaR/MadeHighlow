using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerSimulator
    {
        public RegisterPlayerSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] RegisterPlayerResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RegisterPlayerResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Context.Modifier.CreatePlayer(Initial, Result.Registered);
        }
    }
}
