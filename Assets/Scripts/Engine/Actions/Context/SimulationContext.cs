using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public class SimulationContext : ISimulationContext
    {
        public SimulationContext([NotNull] IWorldFinder finder, [NotNull] IWorldModifier modifier)
        {
            Finder = finder;
            Modifier = modifier;
        }

        public IWorldFinder Finder { get; }
        public IWorldModifier Modifier { get; }
    }
}
