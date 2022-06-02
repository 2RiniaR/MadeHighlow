namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public abstract record CreateComponentResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new CreateComponentSimulator(context, world, this).Simulate();
        }
    }
}
