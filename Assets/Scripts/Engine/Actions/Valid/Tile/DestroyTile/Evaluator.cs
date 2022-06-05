using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
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
            DeleteTarget();

            if (Result.DeleteTile.Content.Deleted == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        private void DeleteTarget()
        {
            var action = new DeleteTile.Action(Action.TargetID);
            var result = Context.Actions.DeleteTile(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { DeleteTile = @event };
        }

        private void Confirm()
        {
            Result = Result with { Deleted = Action.TargetID };
        }
    }
}
