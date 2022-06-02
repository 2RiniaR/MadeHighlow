namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public abstract record DeleteComponentResult : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new DeleteComponentSimulator(context, world, this).Simulate();
        }
    }
}
