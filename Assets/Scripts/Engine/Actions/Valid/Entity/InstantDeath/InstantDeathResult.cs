namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public abstract record InstantDeathResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new InstantDeathSimulator(context, world, this).Simulate();
        }
    }
}
