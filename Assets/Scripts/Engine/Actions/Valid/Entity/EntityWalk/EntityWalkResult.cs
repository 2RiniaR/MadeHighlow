namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public abstract record EntityWalkResult : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new EntityWalkSimulator(context, world, this).Simulate();
        }
    }
}
