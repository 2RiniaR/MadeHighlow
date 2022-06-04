namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public abstract record Result : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
