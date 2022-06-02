namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public abstract record RegisterComponentResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new RegisterComponentSimulator(context, world, this).Simulate();
        }
    }
}
