using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ComponentID AllowedID { get; init; }
        public ComponentID DisallowedID { get; init; }
    }
}
