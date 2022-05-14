using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.FragmentActions
{
    public record AllocateIDAction
    {
        public AllocateIDResult Evaluate(IActionContext context)
        {
            var latestID = context.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
