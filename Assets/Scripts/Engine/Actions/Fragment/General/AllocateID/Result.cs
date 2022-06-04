namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public record Result(ID AllocatedID) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
