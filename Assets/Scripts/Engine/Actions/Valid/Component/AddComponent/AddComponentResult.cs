namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public abstract record AddComponentResult : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new AddComponentSimulator(context, world, this).Simulate();
        }
    }
}
