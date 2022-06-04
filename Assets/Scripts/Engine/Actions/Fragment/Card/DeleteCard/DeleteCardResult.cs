namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public abstract record DeleteCardResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new DeleteCardSimulator(context, world, this).Simulate();
        }
    }
}
