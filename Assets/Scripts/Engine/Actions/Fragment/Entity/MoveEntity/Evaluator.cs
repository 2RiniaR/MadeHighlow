using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
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
            var target = FindTarget();

            if (target == null) return Result;

            Position(target);
            if (Result.PositionEntity.Content.Positioned == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private void Position([NotNull] Entity target)
        {
            var action = new PositionEntity.Action(
                Action.TargetID,
                target.Position3D.MoveTo(Action.Direction, new Distance(1))
            );
            var result = Context.Actions.PositionEntity(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { PositionEntity = @event };
        }

        private void Confirm()
        {
            Result = Result with { Moved = Result.PositionEntity.Content.Positioned };
        }
    }
}
