using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            if (!IsValid()) return Result;

            ActuateCommand();
            PayUsedCard();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        private bool IsValid()
        {
            var unit = Context.Finder.FindUnit(Initial.World, Action.Command.UnitID);
            if (unit == null) return false;
            if (unit.IsDead) return false;

            var card = Context.Finder.FindCard(Initial.World, Action.Command.CardID);
            if (card == null) return false;

            var player = Context.Finder.FindPlayer(Initial.World, card.OwnerPlayerID);
            if (player == null) return false;
            if (unit.FollowingID != player.PlayerID) return false;

            return true;
        }

        private void ActuateCommand()
        {
            var actions = Action.Command.ActionsIn(Simulating);
            var simulating = Simulating;
            var events = Context.Flows.IterateActions(ref simulating, actions);
            Simulating = simulating;
            Result = Result with { CommandActions = events };
        }

        private void PayUsedCard()
        {
            var action = new PayCard.Action(Action.Command.CardID);
            var result = Context.Actions.PayCard(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { PayCard = @event };
        }

        private void Confirm()
        {
            Result = Result with { Run = true };
        }
    }
}
