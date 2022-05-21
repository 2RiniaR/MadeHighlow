using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.AllocateID
{
    public record AllocateIDResult([NotNull] AllocateIDAction Action, ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}
