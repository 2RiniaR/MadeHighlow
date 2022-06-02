namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public abstract record RunCommandResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new RunCommandSimulator(context, world, this).Simulate();
        }
    }
}
