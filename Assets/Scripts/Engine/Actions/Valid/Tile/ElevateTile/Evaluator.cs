using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var target = FindTarget();

            if (target == null) return Result;

            // TODO: タイルの上にいるエンティティのZ座標も変える必要がある

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm(target);

            return Result;
        }

        [CanBeNull]
        private Tile FindTarget()
        {
            return Context.Finder.FindTile(Initial.World, Action.TargetID);
        }

        private void Confirm([NotNull] Tile target)
        {
            Result = Result with { Elevated = target with { Elevation = Action.Elevate.Caused(target.Elevation) } };
        }
    }
}
