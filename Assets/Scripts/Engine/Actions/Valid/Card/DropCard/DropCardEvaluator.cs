using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public class DropCardEvaluator
    {
        public DropCardEvaluator([NotNull] IHistory history, [NotNull] CardID targetID)
        {
            History = history;
            TargetID = targetID;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private CardID TargetID { get; }

        [CanBeNull] private ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults { get; set; }
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

            Target = TargetID.GetFrom(History.World);
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

            var effectors = Component.GetAllOfTypeFrom<IDropCardEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDropCard(History, Target)).Sort();
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
        private DropCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
