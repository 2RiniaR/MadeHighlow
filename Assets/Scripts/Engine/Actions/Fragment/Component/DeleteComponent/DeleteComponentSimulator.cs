using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public class DeleteComponentSimulator
    {
        public DeleteComponentSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] DeleteComponentResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private DeleteComponentResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.DeleteComponent(Initial, succeedResult.Action.TargetID);
            }

            return Initial;
        }
    }
}
