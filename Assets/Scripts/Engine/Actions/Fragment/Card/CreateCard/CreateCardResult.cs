namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public abstract record CreateCardResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new CreateCardSimulator(context, world, this).Simulate();
        }
    }
}
