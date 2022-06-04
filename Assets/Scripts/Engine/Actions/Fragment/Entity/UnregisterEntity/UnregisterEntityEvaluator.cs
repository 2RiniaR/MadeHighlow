using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public class UnregisterEntityEvaluator
    {
        public UnregisterEntityEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] UnregisterEntityAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private UnregisterEntityAction Action { get; }

        [NotNull]
        public UnregisterEntityResult Evaluate()
        {
            UnregisterEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private UnregisterEntityResult FindTarget()
        {
            if (Context.Finder.FindEntity(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private UnregisterEntityResult Succeed()
        {
            return new SucceedResult(Action);
        }
    }
}
