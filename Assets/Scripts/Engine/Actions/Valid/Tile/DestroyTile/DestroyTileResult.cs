namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public abstract record DestroyTileResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new DestroyTileSimulator(context, world, this).Simulate();
        }
    }
}
