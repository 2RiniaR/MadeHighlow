using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public class DestroyEntityEvaluator
    {
        public DestroyEntityEvaluator([NotNull] IActionContext context, [NotNull] EntityID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private EntityID TargetID { get; }

        [CanBeNull] private ValueList<RemoveComponent.SucceedResult> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyEntityEffect>> Interrupts { get; set; }
        [CanBeNull] private Entity Target { get; set; }

        [NotNull]
        public DestroyEntityResult Evaluate()
        {
            Contract.Ensures(Contract.Result<DestroyEntityResult>() != null);

            DestroyEntityResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = CheckRemovable();
            if (result != null) return result;

            result = RemoveComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyEntityResult GetTarget()
        {
            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private DestroyEntityResult CollectInterrupts()
        {
            Contract.Requires<ArgumentNullException>(Target != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyEntityEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDestroyEntity(Context, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Target, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private DestroyEntityResult CheckRemovable()
        {
            Contract.Requires<ArgumentNullException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return null;
        }

        [CanBeNull]
        private DestroyEntityResult RemoveComponents()
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
        private DestroyEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
