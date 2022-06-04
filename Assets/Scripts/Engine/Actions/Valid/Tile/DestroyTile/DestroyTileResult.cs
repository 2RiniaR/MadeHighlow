namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public abstract record DestroyTileResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new DestroyTileSimulator(context, world, this).Simulate();
        }
    }
}
