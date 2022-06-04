namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public abstract record SupplyCardResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new SupplyCardSimulator(context, world, this).Simulate();
        }
    }
}
