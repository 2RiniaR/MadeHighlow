using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent.RegisterComponent
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Component Registered { get; init; }
    }
}
