namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public abstract record ElevateTileResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new ElevateTileSimulator(context, world, this).Simulate();
        }
    }
}
