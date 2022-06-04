using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private ValueList<Interrupt<Acceptance>> AcceptanceInterrupts { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = PreValidation();
            if (result != null) return result;

            result = CheckAcceptance();
            if (result != null) return result;

            return Disallowed();
        }

        [CanBeNull]
        private Result PreValidation()
        {
            var card = Context.Finder.FindCard(Initial.World, Action.Command.CardID);
            if (card == null)
            {
                return new FailedResult(Action, FailedReason.CardNotFound);
            }

            var unit = Context.Finder.FindUnit(Initial.World, Action.Command.UnitID);
            if (unit == null)
            {
                return new FailedResult(Action, FailedReason.UnitNotFound);
            }

            var player = Context.Finder.FindPlayer(Initial.World, card.OwnerPlayerID);
            if (player == null)
            {
                return new FailedResult(Action, FailedReason.OwnerNotFound);
            }

            if (unit.FollowingID != player.PlayerID)
            {
                return new FailedResult(Action, FailedReason.NotOwner);
            }

            return null;
        }

        [CanBeNull]
        private Result CheckAcceptance()
        {
            var effectors = Context.Finder.GetAllComponents<IAcceptor>(Initial.World).Sort();

            AcceptanceInterrupts = ValueList<Interrupt<Acceptance>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.ReserveCommandAcceptance(Initial, Action, AcceptanceInterrupts);
                if (interrupt == null) continue;
                AcceptanceInterrupts = AcceptanceInterrupts.Add(interrupt);
            }

            if (AcceptanceInterrupts.IsEmpty) return null;

            var applied = AcceptanceInterrupts[0];
            if (applied.Effect.Allowed)
            {
                return new SucceedResult(Action, AcceptanceInterrupts, applied.ComponentID);
            }

            return new DisallowedResult(Action, AcceptanceInterrupts, applied.ComponentID);
        }

        [NotNull]
        private Result Disallowed()
        {
            return new DisallowedResult(Action, AcceptanceInterrupts, null);
        }
    }
}
