namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public abstract record CreateTileResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new CreateTileSimulator(context, world, this).Simulate();
        }
    }
}
