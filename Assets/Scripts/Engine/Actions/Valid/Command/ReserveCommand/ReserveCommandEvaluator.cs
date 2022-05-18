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

        [CanBeNull] private ValueList<Interrupt<ReserveCommandEffect>> Interrupts { get; set; }

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
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IReserveCommandEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<ReserveCommandEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnReserveCommand(Initial, Action);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private ReserveCommandResult Judge()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is DisallowEffect)
                {
                    return new DisallowedResult(Action, Interrupts, interrupt.ComponentID);
                }

                if (interrupt.Effect is AllowEffect)
                {
                    return new SucceedResult(Action, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private ReserveCommandResult Disallowed()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new DisallowedResult(Action, Interrupts, null);
        }
    }
}
