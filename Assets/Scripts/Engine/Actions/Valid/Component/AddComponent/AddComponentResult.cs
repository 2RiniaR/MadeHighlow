namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public abstract record AddComponentResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new AddComponentSimulator(context, world, this).Simulate();
        }
    }
}
