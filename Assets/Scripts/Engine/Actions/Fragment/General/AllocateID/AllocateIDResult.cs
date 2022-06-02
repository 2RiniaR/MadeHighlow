namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public record AllocateIDResult(ID AllocatedID) : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new AllocateIDSimulator(context, world, this).Simulate();
        }
    }
}
