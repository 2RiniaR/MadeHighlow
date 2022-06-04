namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public record AllocateIDResult(ID AllocatedID) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new AllocateIDSimulator(context, world, this).Simulate();
        }
    }
}
