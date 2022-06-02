namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public abstract record InstantDamageResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new InstantDamageSimulator(context, world, this).Simulate();
        }
    }
}
