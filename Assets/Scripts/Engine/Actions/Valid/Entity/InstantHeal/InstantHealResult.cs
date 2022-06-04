namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public abstract record InstantHealResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new InstantHealSimulator(context, world, this).Simulate();
        }
    }
}
