using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public class RunCommandEvaluator
    {
        public RunCommandEvaluator([NotNull] IActionContext context, [NotNull] Command command)
        {
            Context = context;
            Command = command;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private Command Command { get; }
        [CanBeNull] private ValueList<Interrupt<RunCommandEffect>> Interrupts { get; set; }

        [CanBeNull] private Unit Unit { get; set; }
        [CanBeNull] private Player Player { get; set; }
        [CanBeNull] private Card Card { get; set; }
        [CanBeNull] [ItemNotNull] private ValueList<Result> CommandActionResults { get; set; }
        [CanBeNull] private PayCardResult PayCardResult { get; set; }

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

            Unit = Command.UnitID.GetFrom(Context.World);

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

            Card = Command.CardID.GetFrom(Context.World);
            if (Card == null)
            {
                return new FailedResult(Command, FailedReason.CardNotFound);
            }

            Player = Card.OwnerPlayerID.GetFrom(Context.World);
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

            var effectors = Component.GetAllOfTypeFrom<IRunCommandEffector>(Context.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnRunCommand(Context, Player, Unit, Card, Command)
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

            CommandActionResults = ValueList<Result>.Empty;
            foreach (var action in Command.ActionsIn(Context))
            {
                var result = action.EvaluateAbstract(Context);
                Context = Context.Appended(result);
                CommandActionResults = CommandActionResults.Add(result);
            }
        }

        private void PayUsedCard()
        {
            Contract.Ensures(PayCardResult != null);

            PayCardResult = new PayCardAction(Command.CardID).Evaluate(Context);
            Context = Context.Appended(PayCardResult);
        }

        [NotNull]
        private RunCommandResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(CommandActionResults != null);
            Contract.Requires<InvalidOperationException>(PayCardResult != null);

            return new SucceedResult(Command, Interrupts, PayCardResult, CommandActionResults);
        }
    }
}
