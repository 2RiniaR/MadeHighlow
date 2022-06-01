using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public class RunCommandEvaluator
    {
        public RunCommandEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            RunCommandAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private RunCommandAction Action { get; }

        [CanBeNull] [ItemNotNull] private ValueList<Event<ReactedResult<ValidResult>>> CommandActionEvents { get; set; }
        [CanBeNull] private Event<ReactedResult<PayCardResult>> PayCardEvent { get; set; }
        [CanBeNull] private RunCommandProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<RunCommandRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public RunCommandResult Evaluate()
        {
            RunCommandResult result;

            result = PreValidation();
            if (result != null) return result;

            ActuateCommand();
            PayUsedCard();

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private RunCommandResult PreValidation()
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
            CommandActionEvents = ValueList<Event<ReactedResult<ValidResult>>>.Empty;
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
            var result = Context.Actions.PayCard(Simulating, new PayCardAction(Action.Command.CardID));
            Simulating = Simulating.Appended(result, out var @event);
            PayCardEvent = @event;
        }

        private void WrapProcess()
        {
            Process = new RunCommandProcess(CommandActionEvents, PayCardEvent);
        }

        [CanBeNull]
        private RunCommandResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IRunCommandRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<RunCommandRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.RunCommandRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private RunCommandResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
