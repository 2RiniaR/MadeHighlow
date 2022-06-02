namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public abstract record UnregisterCardResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new UnregisterCardSimulator(context, world, this).Simulate();
        }
    }
}
