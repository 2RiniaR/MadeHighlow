namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public abstract record RegisterCardResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new RegisterCardSimulator(context, world, this).Simulate();
        }
    }
}
