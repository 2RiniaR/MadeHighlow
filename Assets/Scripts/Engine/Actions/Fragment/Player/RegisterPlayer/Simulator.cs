using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class Simulator
    {
        public Simulator([NotNull] ISimulationContext context, [NotNull] World initial, [NotNull] Result result)
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private Result Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result.Registered == null) return Initial;
            return Context.Modifier.CreatePlayer(Initial, Result.Registered);
        }
    }
}
