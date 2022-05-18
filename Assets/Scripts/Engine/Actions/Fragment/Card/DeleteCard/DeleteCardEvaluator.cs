using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public class DeleteCardEvaluator
    {
        public DeleteCardEvaluator([NotNull] IHistory history, [NotNull] DeleteCardAction action)
        {
            History = history;
            Action = action;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private DeleteCardAction Action { get; }

        [NotNull]
        public DeleteCardResult Evaluate()
        {
            DeleteCardResult result;

            result = FindTarget();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DeleteCardResult FindTarget()
        {
            if (Action.TargetID.GetFrom(History.World) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [NotNull]
        private DeleteCardResult Succeed()
        {
            return new SucceedResult(Action);
        }
    }
}
