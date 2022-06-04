namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public abstract record CreateComponentResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new CreateComponentSimulator(context, world, this).Simulate();
        }
    }
}
