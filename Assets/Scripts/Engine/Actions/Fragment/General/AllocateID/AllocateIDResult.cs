using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public record AllocateIDResult([NotNull] AllocateIDAction Action, ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}
