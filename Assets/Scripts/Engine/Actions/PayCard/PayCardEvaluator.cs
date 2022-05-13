using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public class PayCardEvaluator
    {
        public PayCardEvaluator([NotNull] IActionContext context, [NotNull] CardID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private CardID TargetID { get; }

        [CanBeNull] private ValueList<RemoveComponent.SucceedResult> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<PayCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Target { get; set; }

        [NotNull]
        public PayCardResult Evaluate()
        {
            Contract.Ensures(Contract.Result<PayCardResult>() != null);

            PayCardResult error;

            error = GetTarget();
            if (error != null) return error;

            error = CollectInterrupts();
            if (error != null) return error;

            error = CheckRemovable();
            if (error != null) return error;

            error = RemoveComponents();
            if (error != null) return error;

            return Succeed();
        }

        [CanBeNull]
        private PayCardResult GetTarget()
        {
            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private PayCardResult CollectInterrupts()
        {
            Contract.Requires<ArgumentNullException>(Target != null);

            var effectors = Component.GetAllOfTypeFrom<IPayCardEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnPayCard(Context, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is ExemptEffect)
                {
                    return new ExemptedResult(Target, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private PayCardResult CheckRemovable()
        {
            Contract.Requires<ArgumentNullException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return null;
        }

        [CanBeNull]
        private PayCardResult RemoveComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            RemoveComponentResults = ValueList<RemoveComponent.SucceedResult>.Empty;
            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(Context);
                if (result is not RemoveComponent.SucceedResult succeedResult)
                {
                    return new RemoveComponentFailedResult(Target, Interrupts, RemoveComponentResults, result);
                }

                Context = Context.Appended(succeedResult);
                RemoveComponentResults = RemoveComponentResults.Add(succeedResult);
            }

            return null;
        }

        [NotNull]
        private PayCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
