using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity.RegisterEntity
{
    public record Result([NotNull] IAction Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Entity Registered { get; init; }
    }
}
