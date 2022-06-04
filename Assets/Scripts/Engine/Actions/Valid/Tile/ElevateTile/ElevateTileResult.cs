namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public abstract record ElevateTileResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new ElevateTileSimulator(context, world, this).Simulate();
        }
    }
}
