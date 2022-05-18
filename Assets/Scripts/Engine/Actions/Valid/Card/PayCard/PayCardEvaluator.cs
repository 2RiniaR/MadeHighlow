using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

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

        [CanBeNull]
        private ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponentEvents { get; set; }

        [CanBeNull] private Event<Fragment.DeleteCard.SucceedResult> DeleteCardEvent { get; set; }

        [CanBeNull] private Process Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<PayCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Target { get; set; }

        [NotNull]
        public PayCardResult Evaluate()
        {
            PayCardResult result;

            result = FindTarget();
            if (result != null) return result;

            result = RemoveAttachedComponents();
            if (result != null) return result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckExemption();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PayCardResult FindTarget()
        {
            Contract.Ensures((Contract.Result<PayCardResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private PayCardResult RemoveAttachedComponents()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(RemoveComponentEvents != null);

            RemoveComponentEvents = ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>>.Empty;

            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(Simulating);

                var succeedResult = result.BodyAs<RemoveComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new RemoveComponentFailedResult(Action, RemoveComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                RemoveComponentEvents = RemoveComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private PayCardResult DeleteTarget()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentEvents != null);

            var result = new DeleteCardAction(Action.TargetID).Evaluate(Simulating);
            if (result is not Fragment.DeleteCard.SucceedResult succeedResult)
            {
                return new DeleteCardFailedResult(Action, RemoveComponentEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentEvents != null);
            Contract.Requires<InvalidOperationException>(DeleteCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(RemoveComponentEvents, DeleteCardEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IPayCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<PayCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnPayCard(Simulating, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private PayCardResult CheckExemption()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is ExemptEffect)
                {
                    return new RejectedResult(Action, Process, Interrupts, interrupt.ComponentID);
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
