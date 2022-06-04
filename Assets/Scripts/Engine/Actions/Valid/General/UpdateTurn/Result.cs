using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Event<ReactedResult<IValidResult>>> ActorUpdates { get; init; }
        public Event<IncrementTurn.Result> IncrementTurn { get; init; }
    }
}
