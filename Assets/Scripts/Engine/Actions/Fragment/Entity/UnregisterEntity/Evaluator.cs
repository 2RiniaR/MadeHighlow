using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, [NotNull] Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            if (!IsTargetExists()) return Result;

            Confirm();

            return Result;
        }

        private bool IsTargetExists()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID) != null;
        }

        private void Confirm()
        {
            Result = Result with { UnregisteredID = Result.Action.TargetID };
        }
    }
}
