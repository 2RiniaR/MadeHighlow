using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public class ReserveCommandEvaluator
    {
        public ReserveCommandEvaluator([NotNull] IHistory history, [NotNull] Command command)
        {
            History = history;
            Command = command;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private Command Command { get; }
        [CanBeNull] private ValueList<Interrupt<ReserveCommandEffect>> Interrupts { get; set; }

        [CanBeNull] private Unit Unit { get; set; }
        [CanBeNull] private Player Player { get; set; }
        [CanBeNull] private Card Card { get; set; }

        [NotNull]
        public ReserveCommandResult Evaluate()
        {
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
            Contract.Ensures(
                (Contract.Result<ReserveCommandResult>() != null) ^ (Card != null && Unit != null && Player != null)
            );

            Card = Command.CardID.GetFrom(History.World);
            if (Card == null)
            {
                return new FailedResult(Command, FailedReason.CardNotFound);
            }

            Unit = Command.UnitID.GetFrom(History.World);
            if (Unit == null)
            {
                return new FailedResult(Command, FailedReason.UnitNotFound);
            }

            Player = Card.OwnerPlayerID.GetFrom(History.World);
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
            Contract.Requires<InvalidOperationException>(Player != null);
            Contract.Requires<InvalidOperationException>(Unit != null);
            Contract.Requires<InvalidOperationException>(Card != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IReserveCommandEffector>(History.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnReserveCommand(History, Player, Unit, Card, Command)
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
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new DisallowedResult(Command, Interrupts, null);
        }
    }
}
