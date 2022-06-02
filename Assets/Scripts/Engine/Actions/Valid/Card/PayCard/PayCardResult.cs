namespace RineaR.MadeHighlow.Actions.PayCard
{
    public abstract record PayCardResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new PayCardSimulator(context, world, this).Simulate();
        }
    }
}
