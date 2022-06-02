namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public abstract record PositionTileResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new PositionTileSimulator(context, world, this).Simulate();
        }
    }
}
