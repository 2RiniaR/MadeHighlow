using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public class UnregisterCardEvaluator
    {
        public UnregisterCardEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] UnregisterCardAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private UnregisterCardAction Action { get; }

        [NotNull]
        public UnregisterCardResult Evaluate()
        {
            UnregisterCardResult result;

            result = FindTarget();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private UnregisterCardResult FindTarget()
        {
            if (Context.Finder.FindCard(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private UnregisterCardResult Succeed()
        {
            return new SucceedResult(Action);
        }
    }
}
