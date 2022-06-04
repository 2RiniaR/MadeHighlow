using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class RegisterComponentSimulator
    {
        public RegisterComponentSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] RegisterComponentResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RegisterComponentResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.CreateComponent(Initial, succeedResult.Registered);
            }

            return Initial;
        }
    }
}
