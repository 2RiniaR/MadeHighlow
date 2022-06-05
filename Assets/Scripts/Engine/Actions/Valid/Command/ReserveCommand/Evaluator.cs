using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Result = new Result(Action) { IsAllowed = false };
            AcceptanceChecker = new AcceptanceChecker(Context);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull] private AcceptanceChecker AcceptanceChecker { get; }

        [NotNull]
        public Result Evaluate()
        {
            if (!IsValid()) return Result;

            AcceptanceChecker.Check(
                history: Initial,
                contextProvider: (history, collected) => new AcceptanceContext(history, Result, collected),
                onApplied: acceptance =>
                {
                    Result = Result with { Applied = acceptance.Applied, IsAllowed = acceptance.IsAllowed };
                }
            );

            if (!Result.IsAllowed) return Result;

            Confirm();

            return Result;
        }

        private bool IsValid()
        {
            var card = Context.Finder.FindCard(Initial.World, Action.Command.CardID);
            if (card == null) return false;

            var unit = Context.Finder.FindUnit(Initial.World, Action.Command.UnitID);
            if (unit == null) return false;

            var player = Context.Finder.FindPlayer(Initial.World, card.OwnerPlayerID);
            if (player == null) return false;
            if (unit.FollowingID != player.PlayerID) return false;

            return true;
        }

        private void Confirm()
        {
            Result = Result with { Reserved = Action.Command };
        }
    }
}
