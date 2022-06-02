namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public abstract record SupplyCardResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new SupplyCardSimulator(context, world, this).Simulate();
        }
    }
}
