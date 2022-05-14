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

        [CanBeNull] private ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<PayCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Target { get; set; }

        [NotNull]
        public PayCardResult Evaluate()
        {
            PayCardResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = RemoveAttachedComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PayCardResult GetTarget()
        {
            Contract.Ensures((Contract.Result<PayCardResult>() != null) ^ (Target != null));

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
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

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
        private PayCardResult RemoveAttachedComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(RemoveComponentResults != null);

            RemoveComponentResults = ValueList<ReactedResult<RemoveComponent.SucceedResult>>.Empty;
            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(Context);
                var succeedResult = result.BodyAs<RemoveComponent.SucceedResult>();
                if (succeedResult == null)
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
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
