using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
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
        [NotNull] private Action Action { get; }

        [NotNull] private IHistory Simulating { get; set; }
        private Event<AllocateID.Result> AllocateIDEvent { get; set; }
        private Event<RegisterComponent.SucceedResult> RegisterComponentEvent { get; set; }
        private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            AllocateID();

            result = Register();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) =>
                    result = new RejectedResult(Action, Process, rejection, rejectedID)
            );
            if (result != null) return result;

            return Succeed();
        }

        private void AllocateID()
        {
            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private Result Register()
        {
            var result = Context.Actions.RegisterComponent(
                Simulating,
                new RegisterComponent.Action(Action.TargetID, AllocateIDEvent.Result.AllocatedID, Action.InitialStatus)
            );

            if (result is not RegisterComponent.SucceedResult succeedResult)
            {
                return new RegisterComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            RegisterComponentEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(AllocateIDEvent, RegisterComponentEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
