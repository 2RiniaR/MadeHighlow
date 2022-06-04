using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record Result([NotNull] Action Action, [NotNull] Process Process) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
