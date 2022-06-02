namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public abstract record CreateEntityResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new CreateEntitySimulator(context, world, this).Simulate();
        }
    }
}
