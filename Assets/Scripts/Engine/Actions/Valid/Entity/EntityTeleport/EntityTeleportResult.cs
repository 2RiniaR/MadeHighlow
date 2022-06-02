namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public abstract record EntityTeleportResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new EntityTeleportSimulator(context, world, this).Simulate();
        }
    }
}
