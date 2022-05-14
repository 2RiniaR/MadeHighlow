using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments
{
    public record AllocateIDAction
    {
        public AllocateIDResult Evaluate(IHistory history)
        {
            var latestID = history.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
