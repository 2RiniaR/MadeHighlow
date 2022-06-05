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
            return new Result(Initial.World.NextID);
        }
    }
}
