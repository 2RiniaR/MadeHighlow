namespace RineaR.MadeHighlow.Actions
{
    public class SimulationContext
    {
        public SimulationContext(IWorldFinder finder, IWorldModifier modifier)
        {
            Finder = finder;
            Modifier = modifier;
        }

        public IWorldFinder Finder { get; }
        public IWorldModifier Modifier { get; }
    }
}
