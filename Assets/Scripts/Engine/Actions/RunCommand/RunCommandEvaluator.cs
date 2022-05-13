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
            Contract.Ensures(Contract.Result<RunCommandResult>() != null);

            RunCommandResult result;

            result = PreValidation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            RunCommandAction();
            PayUsedCard();

            return Succeed();
        }

        [CanBeNull]
        private RunCommandResult PreValidation()
        {
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
            Contract.Requires<ArgumentNullException>(Player != null);
            Contract.Requires<ArgumentNullException>(Unit != null);
            Contract.Requires<ArgumentNullException>(Card != null);

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

        private void RunCommandAction()
        {
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
            PayCardResult = new PayCardAction(Command.CardID).Evaluate(Context);
            Context = Context.Appended(PayCardResult);
        }

        [NotNull]
        private RunCommandResult Succeed()
        {
            Contract.Requires<ArgumentNullException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(CommandActionResults != null);
            Contract.Requires<ArgumentNullException>(PayCardResult != null);

            return new SucceedResult(Command, Interrupts, PayCardResult, CommandActionResults);
        }
    }
}
