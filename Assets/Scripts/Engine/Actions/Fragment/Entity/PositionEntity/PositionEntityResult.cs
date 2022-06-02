namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public abstract record PositionEntityResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new PositionEntitySimulator(context, world, this).Simulate();
        }
    }
}
