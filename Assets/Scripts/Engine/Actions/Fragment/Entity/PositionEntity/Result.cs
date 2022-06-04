using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Entity Positioned { get; init; }
    }
}
