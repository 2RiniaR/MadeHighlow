using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PlaceCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public class SupplyCardEvaluator
    {
        public SupplyCardEvaluator([NotNull] IHistory initial, SupplyCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private SupplyCardAction Action { get; }

        [CanBeNull] private Event<PlaceCard.SucceedResult> PlaceCardEvent { get; set; }

        [CanBeNull] private SupplyCardProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<SupplyCardRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public SupplyCardResult Evaluate()
        {
            SupplyCardResult result;

            result = PlaceCard();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private SupplyCardResult PlaceCard()
        {
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (PlaceCardEvent != null));

            var result = new PlaceCardAction(Action.TargetID, Action.InitialStatus).Evaluate(Simulating);
            if (result is not PlaceCard.SucceedResult succeedResult)
            {
                return new PlaceCardFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PlaceCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(PlaceCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new SupplyCardProcess(PlaceCardEvent);
        }

        [CanBeNull]
        private SupplyCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<ISupplyCardRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<SupplyCardRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.SupplyCardRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private SupplyCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
