using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public class DropCardEvaluator
    {
        public DropCardEvaluator([NotNull] IActionContext context, [NotNull] CardID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private CardID TargetID { get; }

        [CanBeNull] private ValueList<RemoveComponent.SucceedResult> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<DropCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Target { get; set; }

        [NotNull]
        public DropCardResult Evaluate()
        {
            DropCardResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = RemoveAttachedComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DropCardResult GetTarget()
        {
            Contract.Ensures((Contract.Result<DropCardResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private DropCardResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDropCardEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDropCard(Context, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectedEffect)
                {
                    return new RejectedResult(Target, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private DropCardResult RemoveAttachedComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(RemoveComponentResults != null);

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
        private DropCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
