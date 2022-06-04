namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public abstract record KnockBackResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new KnockBackSimulator(context, world, this).Simulate();
        }
    }
}
