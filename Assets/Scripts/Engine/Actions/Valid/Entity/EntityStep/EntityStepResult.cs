namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public abstract record EntityStepResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new EntityStepSimulator(context, world, this).Simulate();
        }
    }
}
