using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
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

        [CanBeNull] private Event<DeleteComponent.SucceedResult> DeleteComponentEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = DeleteComponent();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private Result DeleteComponent()
        {
            var result = Context.Actions.DeleteComponent(Simulating, new DeleteComponent.Action(Action.TargetID));
            if (result is not DeleteComponent.SucceedResult succeedResult)
            {
                return new DeleteComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteComponentEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(DeleteComponentEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
