namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public record AllocateIDAction
    {
        public AllocateIDResult Evaluate(IHistory history)
        {
            var latestID = history.World.LatestAllocatedID;
            return new AllocateIDResult(this, ID.From(latestID.InternalValue + 1));
        }
    }
}
