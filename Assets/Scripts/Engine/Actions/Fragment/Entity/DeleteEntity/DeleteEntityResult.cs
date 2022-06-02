namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public abstract record DeleteEntityResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new DeleteEntitySimulator(context, world, this).Simulate();
        }
    }
}
