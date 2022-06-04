using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public class RegisterEntitySimulator
    {
        public RegisterEntitySimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] RegisterEntityResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RegisterEntityResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Context.Modifier.CreateEntity(Initial, Result.Registered);
        }
    }
}
