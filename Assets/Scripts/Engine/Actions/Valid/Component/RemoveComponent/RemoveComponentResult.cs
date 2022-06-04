namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public abstract record RemoveComponentResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new RemoveComponentSimulator(context, world, this).Simulate();
        }
    }
}
