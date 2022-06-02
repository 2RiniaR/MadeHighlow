namespace RineaR.MadeHighlow.Actions.DropCard
{
    public abstract record DropCardResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new DropCardSimulator(context, world, this).Simulate();
        }
    }
}
