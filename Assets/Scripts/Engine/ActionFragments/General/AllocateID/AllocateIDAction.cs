using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments
{
    public record AllocateIDAction
    {
        public AllocateIDResult Evaluate(IHistory context)
        {
            var latestID = context.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
