using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDEvaluator
    {
        public AllocateIDEvaluator([NotNull] IHistory initial)
        {
            Initial = initial;
        }

        [NotNull] private IHistory Initial { get; }

        public AllocateIDResult Evaluate()
        {
            var latestID = Initial.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
