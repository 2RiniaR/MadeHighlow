using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.PayCard;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public class RunCommandEvaluator
    {
        public RunCommandEvaluator([NotNull] IHistory history, [NotNull] Command command)
        {
            History = history;
            Command = command;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private Command Command { get; }
        [CanBeNull] private ValueList<Interrupt<RunCommandEffect>> Interrupts { get; set; }

        [CanBeNull] private Unit Unit { get; set; }
        [CanBeNull] private Player Player { get; set; }
        [CanBeNull] private Card Card { get; set; }
        [CanBeNull] [ItemNotNull] private ValueList<ReactedResult> CommandActionResults { get; set; }
        [CanBeNull] private ReactedResult<PayCardResult> PayCardResult { get; set; }

        [NotNull]
        public RunCommandResult Evaluate()
        {
            RunCommandResult result;

            result = PreValidation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            ActuateCommand();
            PayUsedCard();

            return Succeed();
        }

        [CanBeNull]
        private RunCommandResult PreValidation()
        {
            Contract.Ensures(
                (Contract.Result<RunCommandResult>() != null) ^ (Card != null && Unit != null && Player != null)
            );

            Unit = Command.UnitID.GetFrom(History.World);

            // いないものは行動できない。
            if (Unit == null)
            {
                return new FailedResult(Command, FailedReason.UnitNotFound);
            }

            // 死者は行動できないよ。
            if (Unit.Vitality != null && Unit.Vitality.IsDead)
            {
                return new FailedResult(Command, FailedReason.UnitIsDead);
            }

            Card = Command.CardID.GetFrom(History.World);
            if (Card == null)
            {
                return new FailedResult(Command, FailedReason.CardNotFound);
            }

            Player = Card.OwnerPlayerID.GetFrom(History.World);
            if (Player == null)
            {
                return new FailedResult(Command, FailedReason.PlayerNotFound);
            }

            return null;
        }

        [CanBeNull]
        private RunCommandResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Player != null);
            Contract.Requires<InvalidOperationException>(Unit != null);
            Contract.Requires<InvalidOperationException>(Card != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IRunCommandEffector>(History.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnRunCommand(History, Player, Unit, Card, Command)
                )
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is CancelEffect)
                {
                    return new CanceledResult(Command, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        private void ActuateCommand()
        {
            Contract.Ensures(CommandActionResults != null);

            CommandActionResults = ValueList<ReactedResult>.Empty;
            foreach (var action in Command.ActionsIn(History))
            {
                var result = action.EvaluateBase(History);
                History = History.Appended(result);
                CommandActionResults = CommandActionResults.Add(result);
            }
        }

        private void PayUsedCard()
        {
            Contract.Ensures(PayCardResult != null);

            PayCardResult = new PayCardAction(Command.CardID).Evaluate(History);
            History = History.Appended(PayCardResult);
        }

        [NotNull]
        private RunCommandResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(CommandActionResults != null);
            Contract.Requires<InvalidOperationException>(PayCardResult != null);

            return new SucceedResult(Command, Interrupts, CommandActionResults, PayCardResult);
        }
    }
}
