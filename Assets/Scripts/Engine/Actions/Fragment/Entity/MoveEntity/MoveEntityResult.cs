namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public abstract record MoveEntityResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new MoveEntitySimulator(context, world, this).Simulate();
        }
    }
}
