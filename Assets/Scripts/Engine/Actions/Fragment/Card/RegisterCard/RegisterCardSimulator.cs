using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public class RegisterCardSimulator
    {
        public RegisterCardSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] RegisterCardResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RegisterCardResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.CreateCard(Initial, succeedResult.Registered);
            }

            return Initial;
        }
    }
}
