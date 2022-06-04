using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, [NotNull] Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindTarget();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindTarget()
        {
            if (Context.Finder.FindTile(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action);
        }
    }
}
