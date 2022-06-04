using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Tile Target { get; set; }
        [CanBeNull] private Tile Positioned { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindTarget()
        {
            Target = Context.Finder.FindTile(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.TileNotExist);
            }

            return null;
        }

        [CanBeNull]
        private Result Position()
        {
            if (!IsPositionable(Initial, Action.Destination))
            {
                return new FailedResult(Action, FailedReason.ResolveFailed);
            }

            Positioned = Target;
            return null;
        }

        private static bool IsPositionable([NotNull] IHistory history, [NotNull] Position2D dest)
        {
            return dest.GetTile(history.World) == null;
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Positioned);
        }
    }
}
