using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
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
            CreateTarget();

            if (Result.CreateTile.Content.Created == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        private void CreateTarget()
        {
            var action = new CreateTile.Action(Action.InitialProps);
            var result = Context.Actions.CreateTile(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { CreateTile = @event };
        }

        private void Confirm()
        {
            Result = Result with { Created = Action.InitialProps };
        }
    }
}
