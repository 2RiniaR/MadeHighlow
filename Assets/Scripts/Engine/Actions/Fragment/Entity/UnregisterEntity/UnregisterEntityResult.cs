namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public abstract record UnregisterEntityResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new UnregisterEntitySimulator(context, world, this).Simulate();
        }
    }
}
