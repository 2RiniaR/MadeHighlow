using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.PayCard;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public class RunCommandEvaluator
    {
        public RunCommandEvaluator([NotNull] IHistory initial, RunCommandAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private RunCommandAction Action { get; }

        [CanBeNull] [ItemNotNull] private ValueList<Event<ReactedResult>> CommandActionEvents { get; set; }
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
            var unit = Action.Command.UnitID.GetFrom(Initial.World);

            if (unit == null)
            {
                return new FailedResult(Action, FailedReason.UnitNotFound);
            }

            if (unit.Vitality != null && unit.Vitality.IsDead)
            {
                return new FailedResult(Action, FailedReason.UnitIsDead);
            }

            var card = Action.Command.CardID.GetFrom(Initial.World);
            if (card == null)
            {
                return new FailedResult(Action, FailedReason.CardNotFound);
            }

            var player = card.OwnerPlayerID.GetFrom(Initial.World);
            if (player == null)
            {
                return new FailedResult(Action, FailedReason.PlayerNotFound);
            }

            if (unit.FollowingPlayerID != player.PlayerID)
            {
                return new FailedResult(Action, FailedReason.NotOwner);
            }

            return null;
        }

        private void ActuateCommand()
        {
            Contract.Ensures(CommandActionEvents != null);

            CommandActionEvents = ValueList<Event<ReactedResult>>.Empty;
            var actions = Action.Command.ActionsIn(Simulating);

            foreach (var action in actions)
            {
                var result = action.EvaluateBase(Simulating);
                Simulating = Simulating.Appended(result, out var @event);
                CommandActionEvents = CommandActionEvents.Add(@event);
            }
        }

        private void PayUsedCard()
        {
            Contract.Ensures(PayCardEvent != null);

            var result = new PayCardAction(Action.Command.CardID).Evaluate(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            PayCardEvent = @event;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(CommandActionEvents != null);
            Contract.Requires<InvalidOperationException>(PayCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new RunCommandProcess(CommandActionEvents, PayCardEvent);
        }

        [CanBeNull]
        private RunCommandResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IRunCommandRejector>(Initial.World).Sort();

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
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
