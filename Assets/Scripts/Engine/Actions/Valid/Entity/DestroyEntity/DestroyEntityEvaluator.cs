using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public class DestroyEntityEvaluator
    {
        public DestroyEntityEvaluator([NotNull] IHistory history, [NotNull] EntityID targetID)
        {
            History = history;
            TargetID = targetID;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private EntityID TargetID { get; }

        [CanBeNull] private ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyEntityEffect>> Interrupts { get; set; }
        [CanBeNull] private Entity Target { get; set; }

        [NotNull]
        public DestroyEntityResult Evaluate()
        {
            DestroyEntityResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = RemoveAttachedComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyEntityResult GetTarget()
        {
            Contract.Ensures((Contract.Result<DestroyEntityResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private DestroyEntityResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyEntityEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDestroyEntity(History, Target)).Sort();
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
        private DestroyEntityResult RemoveAttachedComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(RemoveComponentResults != null);

            RemoveComponentResults = ValueList<ReactedResult<RemoveComponent.SucceedResult>>.Empty;
            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(History);
                var succeedResult = result.BodyAs<RemoveComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new RemoveComponentFailedResult(Target, Interrupts, RemoveComponentResults, result);
                }

                History = History.Appended(succeedResult);
                RemoveComponentResults = RemoveComponentResults.Add(succeedResult);
            }

            return null;
        }

        [NotNull]
        private DestroyEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
