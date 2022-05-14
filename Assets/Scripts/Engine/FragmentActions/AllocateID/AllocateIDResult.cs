using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.FragmentActions
{
    public record AllocateIDResult(ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}
