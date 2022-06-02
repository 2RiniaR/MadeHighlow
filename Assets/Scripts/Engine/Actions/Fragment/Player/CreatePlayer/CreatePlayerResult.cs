namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public abstract record CreatePlayerResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new CreatePlayerSimulator(context, world, this).Simulate();
        }
    }
}
