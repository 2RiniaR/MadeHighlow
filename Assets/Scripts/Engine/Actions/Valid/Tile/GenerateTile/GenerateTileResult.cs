namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public abstract record GenerateTileResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new GenerateTileSimulator(context, world, this).Simulate();
        }
    }
}
