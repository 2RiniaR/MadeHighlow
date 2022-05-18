using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PlaceCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
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

        [CanBeNull] private Event<Fragment.PlaceCard.SucceedResult> PlaceCardEvent { get; set; }

        [CanBeNull] private Process Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<SupplyCardEffect>> Interrupts { get; set; }

        [NotNull]
        public SupplyCardResult Evaluate()
        {
            SupplyCardResult result;

            result = PlaceCard();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private SupplyCardResult PlaceCard()
        {
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (PlaceCardEvent != null));

            var result = new PlaceCardAction(Action.TargetID, Action.InitialStatus).Evaluate(Simulating);
            if (result is not Fragment.PlaceCard.SucceedResult succeedResult)
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

            Process = new Process(PlaceCardEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<ISupplyCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<SupplyCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnSupplyCard(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private SupplyCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private SupplyCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
