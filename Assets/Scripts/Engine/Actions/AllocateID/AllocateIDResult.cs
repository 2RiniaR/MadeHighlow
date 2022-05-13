namespace RineaR.MadeHighlow.Actions
{
    public record AllocateIDResult(ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}
