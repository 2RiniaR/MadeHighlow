namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public abstract record PositionTileResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new PositionTileSimulator(context, world, this).Simulate();
        }
    }
}
