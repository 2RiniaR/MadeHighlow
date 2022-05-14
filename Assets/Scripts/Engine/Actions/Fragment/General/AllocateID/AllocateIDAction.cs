namespace RineaR.MadeHighlow.Actions.Fragment
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
