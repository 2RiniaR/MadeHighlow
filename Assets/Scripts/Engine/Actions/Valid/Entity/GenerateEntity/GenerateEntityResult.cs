namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public abstract record GenerateEntityResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new GenerateEntitySimulator(context, world, this).Simulate();
        }
    }
}
