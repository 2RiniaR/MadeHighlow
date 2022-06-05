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
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            CreateComponent();
            return Result;
        }

        private void CreateComponent()
        {
            var action = new CreateComponent.Action(Action.TargetID, Action.InitialStatus);
            var result = Context.Actions.CreateComponent(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { CreateComponent = @event };
        }
    }
}
