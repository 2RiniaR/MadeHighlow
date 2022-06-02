namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public abstract record ReserveCommandResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new ReserveCommandSimulator(context, world, this).Simulate();
        }
    }
}
