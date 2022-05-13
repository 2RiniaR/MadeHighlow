using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public class ReserveCommandEvaluator
    {
        public ReserveCommandEvaluator([NotNull] IActionContext context, [NotNull] Command command)
        {
            Context = context;
            Command = command;
        }

        [NotNull] private IActionContext Context { get; }
        [NotNull] private Command Command { get; }
        [CanBeNull] private ValueList<Interrupt<ReserveCommandEffect>> Interrupts { get; set; }

        [CanBeNull] private Unit Unit { get; set; }
        [CanBeNull] private Player Player { get; set; }
        [CanBeNull] private Card Card { get; set; }

        [NotNull]
        public ReserveCommandResult Evaluate()
        {
            Contract.Ensures(Contract.Result<ReserveCommandResult>() != null);

            ReserveCommandResult result;

            result = PreValidation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Disallowed();
        }

        [CanBeNull]
        private ReserveCommandResult PreValidation()
        {
            Card = Command.CardID.GetFrom(Context.World);
            if (Card == null)
            {
                return new FailedResult(Command, FailedReason.CardNotFound);
            }

            Unit = Command.UnitID.GetFrom(Context.World);
            if (Unit == null)
            {
                return new FailedResult(Command, FailedReason.UnitNotFound);
            }

            Player = Card.OwnerPlayerID.GetFrom(Context.World);
            if (Player == null)
            {
                return new FailedResult(Command, FailedReason.OwnerNotFound);
            }

            if (Unit.FollowingPlayerID != Player.PlayerID)
            {
                return new FailedResult(Command, FailedReason.NotOwner);
            }

            return null;
        }

        [CanBeNull]
        private ReserveCommandResult CollectInterrupts()
        {
            Contract.Requires<ArgumentNullException>(Player != null);
            Contract.Requires<ArgumentNullException>(Unit != null);
            Contract.Requires<ArgumentNullException>(Card != null);

            var effectors = Component.GetAllOfTypeFrom<IReserveCommandEffector>(Context.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnReserveCommand(Context, Player, Unit, Card, Command)
                )
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is DisallowEffect)
                {
                    return new DisallowedResult(Command, Interrupts, interrupt.ComponentID);
                }

                if (interrupt.Effect is AllowEffect)
                {
                    return new SucceedResult(Command, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private ReserveCommandResult Disallowed()
        {
            Contract.Requires<ArgumentNullException>(Interrupts != null);

            return new DisallowedResult(Command, Interrupts, null);
        }
    }
}
