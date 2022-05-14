using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public class RemoveComponentEvaluator
    {
        public RemoveComponentEvaluator([NotNull] IHistory history, [NotNull] ComponentID targetID)
        {
            History = history;
            TargetID = targetID;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private ComponentID TargetID { get; }

        [CanBeNull] private ValueList<ReactedResult> FinalizeComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<RemoveComponentEffect>> Interrupts { get; set; }
        [CanBeNull] private Component Target { get; set; }

        [NotNull]
        public RemoveComponentResult Evaluate()
        {
            RemoveComponentResult result;

            result = GetTarget();
            if (result != null) return result;

            result = FinalizeComponent();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        private RemoveComponentResult GetTarget()
        {
            Contract.Ensures((Contract.Result<RemoveComponentResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        private RemoveComponentResult FinalizeComponent()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(FinalizeComponentResults != null);

            var actionConfirmations = Target.InitializeActions(History);

            FinalizeComponentResults = ValueList<ReactedResult>.Empty;
            foreach (var actionConfirmation in actionConfirmations)
            {
                var result = actionConfirmation.Action.EvaluateBase(History);
                if (!actionConfirmation.Confirmation(result))
                {
                    return new FinalizeFailedResult(Target, FinalizeComponentResults, result);
                }

                FinalizeComponentResults = FinalizeComponentResults.Add(result);
                History = History.Appended(result);
            }

            return null;
        }

        private RemoveComponentResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(FinalizeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IRemoveComponentEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnRemoveComponent(History, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Target, FinalizeComponentResults, Interrupts, TargetID);
                }
            }

            return null;
        }

        private RemoveComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(FinalizeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, FinalizeComponentResults, Interrupts);
        }
    }
}
