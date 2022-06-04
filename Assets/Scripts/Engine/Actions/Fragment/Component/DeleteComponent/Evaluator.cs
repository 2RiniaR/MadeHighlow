using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        private ValueList<Interrupt<RejectionContext>> RejectionInterrupts { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = CheckTargetExist();
            if (result != null) return result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(Action, rejection, rejectedID)
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result CheckTargetExist()
        {
            if (Context.Finder.FindComponent(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
