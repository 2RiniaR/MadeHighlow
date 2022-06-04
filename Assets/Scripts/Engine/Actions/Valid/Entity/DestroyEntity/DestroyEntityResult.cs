namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public abstract record DestroyEntityResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new DestroyEntitySimulator(context, world, this).Simulate();
        }
    }
}
