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
            if (IsTileExistAtSamePosition()) return Result;

            Confirm(target);

            return Result;
        }

        [CanBeNull]
        private Tile FindTarget()
        {
            return Context.Finder.FindTile(Initial.World, Action.TargetID);
        }

        private bool IsTileExistAtSamePosition()
        {
            return Context.Finder.FindTile(Initial.World, Action.Destination) != null;
        }

        private void Confirm([NotNull] Tile target)
        {
            Result = Result with { Positioned = target with { Position2D = Action.Destination } };
        }
    }
}
