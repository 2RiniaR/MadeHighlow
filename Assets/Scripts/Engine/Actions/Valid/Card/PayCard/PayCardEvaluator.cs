using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public class PayCardEvaluator
    {
        public PayCardEvaluator([NotNull] IHistory initial, PayCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private PayCardAction Action { get; }

        [CanBeNull] private Event<Fragment.DeleteCard.SucceedResult> DeleteCardEvent { get; set; }
        [CanBeNull] private PayCardProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<PayCardEffect>> Interrupts { get; set; }

        [NotNull]
        public PayCardResult Evaluate()
        {
            PayCardResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        private PayCardResult DeleteTarget()
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

            Process = new PayCardProcess(DeleteCardEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IPayCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<PayCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnPayCard(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private PayCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is ExemptEffect)
                {
                    return new ExemptedResult(Action, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private PayCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
