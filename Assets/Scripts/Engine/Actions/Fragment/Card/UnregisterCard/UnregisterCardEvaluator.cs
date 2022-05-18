using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterCard
{
    public class UnregisterCardEvaluator
    {
        public UnregisterCardEvaluator([NotNull] IHistory initial, [NotNull] UnregisterCardAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            if (Action.TargetID.GetFrom(Initial.World) == null)
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
