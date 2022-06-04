namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public abstract record Result : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
