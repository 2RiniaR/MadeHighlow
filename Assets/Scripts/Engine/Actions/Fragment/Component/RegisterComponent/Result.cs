namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public abstract record Result : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
