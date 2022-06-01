namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDEvaluator
    {
        public AllocateIDResult Evaluate(IHistory history)
        {
            var latestID = history.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
