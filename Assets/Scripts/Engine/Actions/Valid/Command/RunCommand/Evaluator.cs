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
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<IValidResult>>> CommandActionEvents { get; set; }

        [CanBeNull] private Event<ReactedResult<PayCard.Result>> PayCardEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = PreValidation();
            if (result != null) return result;

            ActuateCommand();
            PayUsedCard();

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result PreValidation()
        {
            var unit = Context.Finder.FindUnit(Initial.World, Action.Command.UnitID);

            if (unit == null)
            {
                return new FailedResult(Action, FailedReason.UnitNotFound);
            }

            if (unit.IsDead)
            {
                return new FailedResult(Action, FailedReason.UnitIsDead);
            }

            var card = Context.Finder.FindCard(Initial.World, Action.Command.CardID);
            if (card == null)
            {
                return new FailedResult(Action, FailedReason.CardNotFound);
            }

            var player = Context.Finder.FindPlayer(Initial.World, card.OwnerPlayerID);
            if (player == null)
            {
                return new FailedResult(Action, FailedReason.PlayerNotFound);
            }

            if (unit.FollowingID != player.PlayerID)
            {
                return new FailedResult(Action, FailedReason.NotOwner);
            }

            return null;
        }

        private void ActuateCommand()
        {
            CommandActionEvents = ValueList<Event<ReactedResult<IValidResult>>>.Empty;
            var actions = Action.Command.ActionsIn(Simulating);

            foreach (var action in actions)
            {
                var result = Context.Actions.Run(Simulating, action);
                Simulating = Simulating.Appended(result, out var @event);
                CommandActionEvents = CommandActionEvents.Add(@event);
            }
        }

        private void PayUsedCard()
        {
            var result = Context.Actions.PayCard(Simulating, new PayCard.Action(Action.Command.CardID));
            Simulating = Simulating.Appended(result, out var @event);
            PayCardEvent = @event;
        }

        private void WrapProcess()
        {
            Process = new Process(CommandActionEvents, PayCardEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
