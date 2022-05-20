using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public class DropCardEvaluator
    {
        public DropCardEvaluator([NotNull] IHistory initial, DropCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DropCardAction Action { get; }

        [CanBeNull] private Event<Fragment.DeleteCard.SucceedResult> DeleteCardEvent { get; set; }
        [CanBeNull] private DropCardProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<DropCardEffect>> Interrupts { get; set; }

        [NotNull]
        public DropCardResult Evaluate()
        {
            DropCardResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        private DropCardResult DeleteTarget()
        {
            var result = new DeleteCardAction(Action.TargetID).Evaluate(Simulating);
            if (result is not Fragment.DeleteCard.SucceedResult succeedResult)
            {
                return new DeleteCardFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(DeleteCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new DropCardProcess(DeleteCardEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDropCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<DropCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnDropCard(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private DropCardResult CheckRejection()
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
        private DropCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
