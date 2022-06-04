using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Event<DeleteCard.SucceedResult> DeleteCardEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) => result = new ExemptedResult(
                    Action,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        private Result DeleteTarget()
        {
            var result = Context.Actions.DeleteCard(Simulating, new DeleteCard.Action(Action.TargetID));
            if (result is not DeleteCard.SucceedResult succeedResult)
            {
                return new DeleteCardFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(DeleteCardEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
