namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public abstract record PlaceCardResult : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new PlaceCardSimulator(context, world, this).Simulate();
        }
    }
}
