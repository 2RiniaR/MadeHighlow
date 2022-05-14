namespace RineaR.MadeHighlow.Actions.Fragment
{
    public record AllocateIDResult(ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}
