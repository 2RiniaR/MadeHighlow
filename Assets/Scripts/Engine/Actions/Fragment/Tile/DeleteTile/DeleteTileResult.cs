namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public abstract record DeleteTileResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new DeleteTileSimulator(context, world, this).Simulate();
        }
    }
}
