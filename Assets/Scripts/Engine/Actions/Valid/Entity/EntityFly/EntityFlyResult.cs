namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public abstract record EntityFlyResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new EntityFlySimulator(context, world, this).Simulate();
        }
    }
}
