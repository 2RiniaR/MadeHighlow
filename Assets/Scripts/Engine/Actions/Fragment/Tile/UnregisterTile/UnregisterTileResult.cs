namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public abstract record UnregisterTileResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new UnregisterTileSimulator(context, world, this).Simulate();
        }
    }
}
