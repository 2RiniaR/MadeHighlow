namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public abstract record EntityFlyResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new EntityFlySimulator(context, world, this).Simulate();
        }
    }
}
