namespace RineaR.MadeHighlow.Actions
{
    public record AllocateIDAction : Action<AllocateIDResult>
    {
        public override AllocateIDResult Evaluate(IActionContext context)
        {
            var latestID = context.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
