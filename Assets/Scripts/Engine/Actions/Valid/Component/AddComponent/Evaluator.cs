using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
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

        [CanBeNull] private Event<CreateComponent.SucceedResult> CreateComponentEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = CreateComponent();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private Result CreateComponent()
        {
            var result = Context.Actions.CreateComponent(
                Simulating,
                new CreateComponent.Action(Action.TargetID, Action.InitialStatus)
            );
            if (result is not CreateComponent.SucceedResult succeedResult)
            {
                return new CreateComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateComponentEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(CreateComponentEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
