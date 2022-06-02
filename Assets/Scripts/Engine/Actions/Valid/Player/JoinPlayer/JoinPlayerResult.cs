namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public abstract record JoinPlayerResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new JoinPlayerSimulator(context, world, this).Simulate();
        }
    }
}
