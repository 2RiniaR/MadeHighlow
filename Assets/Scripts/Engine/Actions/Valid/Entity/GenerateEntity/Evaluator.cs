using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
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

        [CanBeNull] private Event<CreateEntity.SucceedResult> CreateEntityEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = CreateTarget();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result CreateTarget()
        {
            var result = Context.Actions.CreateEntity(Simulating, new CreateEntity.Action(Action.InitialProps));
            if (result is not CreateEntity.SucceedResult succeedResult)
            {
                return new CreateEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(CreateEntityEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
