using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<CreateComponent.Result> CreateComponent { get; init; }
    }
}
