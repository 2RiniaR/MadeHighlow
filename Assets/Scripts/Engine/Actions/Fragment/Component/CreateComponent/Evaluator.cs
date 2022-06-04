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
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            AllocateID();
            Register();

            if (Result.RegisterComponent.Content.Registered == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        private void AllocateID()
        {
            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { AllocateID = @event };
        }

        private void Register()
        {
            var action = new RegisterComponent.Action(
                Action.TargetID,
                Result.AllocateID.Content.Allocated,
                Action.InitialStatus
            );
            var result = Context.Actions.RegisterComponent(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { RegisterComponent = @event };
        }

        private void Confirm()
        {
            Result = Result with { Created = Result.RegisterComponent.Content.Registered };
        }
    }
}
