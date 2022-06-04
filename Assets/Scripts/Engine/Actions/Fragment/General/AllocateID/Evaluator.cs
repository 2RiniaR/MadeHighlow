using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class Evaluator
    {
        public Evaluator([NotNull] IHistory initial)
        {
            Initial = initial;
        }

        [NotNull] private IHistory Initial { get; }

        public Result Evaluate()
        {
            var latestID = Initial.World.LatestAllocatedID;
            return new Result(ID.From(latestID.InternalValue + 1));
        }
    }
}
