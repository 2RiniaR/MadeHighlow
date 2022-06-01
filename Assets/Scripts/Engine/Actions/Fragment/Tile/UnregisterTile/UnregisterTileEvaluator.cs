using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public class UnregisterTileEvaluator
    {
        public UnregisterTileEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] UnregisterTileAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private UnregisterTileAction Action { get; }

        [NotNull]
        public UnregisterTileResult Evaluate()
        {
            UnregisterTileResult result;

            result = FindTarget();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private UnregisterTileResult FindTarget()
        {
            if (Context.Finder.FindTile(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private UnregisterTileResult Succeed()
        {
            return new SucceedResult(Action);
        }
    }
}
