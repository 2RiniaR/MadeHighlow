using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public class ReserveCommandEvaluator
    {
        public ReserveCommandEvaluator([NotNull] IHistory initial, ReserveCommandAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private ReserveCommandAction Action { get; }

        [CanBeNull] private ValueList<Interrupt<ReserveCommandAcceptance>> AcceptanceInterrupts { get; set; }

        [NotNull]
        public ReserveCommandResult Evaluate()
        {
            ReserveCommandResult result;

            result = PreValidation();
            if (result != null) return result;

            CollectInterrupts();

            result = Judge();
            if (result != null) return result;

            return Disallowed();
        }

        [CanBeNull]
        private ReserveCommandResult PreValidation()
        {
            var card = Action.Command.CardID.GetFrom(Initial.World);
            if (card == null)
            {
                return new FailedResult(Action, FailedReason.CardNotFound);
            }

            var unit = Action.Command.UnitID.GetFrom(Initial.World);
            if (unit == null)
            {
                return new FailedResult(Action, FailedReason.UnitNotFound);
            }

            var player = card.OwnerPlayerID.GetFrom(Initial.World);
            if (player == null)
            {
                return new FailedResult(Action, FailedReason.OwnerNotFound);
            }

            if (unit.FollowingPlayerID != player.PlayerID)
            {
                return new FailedResult(Action, FailedReason.NotOwner);
            }

            return null;
        }

        private void CollectInterrupts()
        {
            Contract.Ensures(AcceptanceInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IReserveCommandAcceptor>(Initial.World).Sort();

            AcceptanceInterrupts = ValueList<Interrupt<ReserveCommandAcceptance>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.ReserveCommandAcceptance(Initial, Action, AcceptanceInterrupts);
                AcceptanceInterrupts = AcceptanceInterrupts.Add(interrupts);
            }
        }

        [CanBeNull]
        private ReserveCommandResult Judge()
        {
            Contract.Requires<InvalidOperationException>(AcceptanceInterrupts != null);

            if (AcceptanceInterrupts.IsEmpty) return null;

            var applied = AcceptanceInterrupts[0];
            if (applied.Effect.Allowed)
            {
                return new SucceedResult(Action, AcceptanceInterrupts, applied.ComponentID);
            }

            return new DisallowedResult(Action, AcceptanceInterrupts, applied.ComponentID);
        }

        [NotNull]
        private ReserveCommandResult Disallowed()
        {
            Contract.Requires<InvalidOperationException>(AcceptanceInterrupts != null);

            return new DisallowedResult(Action, AcceptanceInterrupts, null);
        }
    }
}
