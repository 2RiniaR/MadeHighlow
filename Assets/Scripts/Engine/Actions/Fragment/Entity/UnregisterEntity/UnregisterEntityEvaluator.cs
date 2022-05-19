using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterEntity
{
    public class UnregisterEntityEvaluator
    {
        public UnregisterEntityEvaluator([NotNull] IHistory initial, [NotNull] UnregisterEntityAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            if (Action.TargetID.GetFrom(Initial.World) == null)
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
