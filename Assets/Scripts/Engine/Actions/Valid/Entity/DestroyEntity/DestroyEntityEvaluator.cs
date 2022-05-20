using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteEntity;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public class DestroyEntityEvaluator
    {
        public DestroyEntityEvaluator([NotNull] IHistory initial, DestroyEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DestroyEntityAction Action { get; }

        [CanBeNull] private Event<Fragment.DeleteEntity.SucceedResult> DeleteEntityEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyEntityEffect>> Interrupts { get; set; }

        [NotNull]
        public DestroyEntityResult Evaluate()
        {
            DestroyEntityResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyEntityResult DeleteTarget()
        {
            Contract.Ensures((Contract.Result<DestroyEntityResult>() != null) ^ (DeleteEntityEvent != null));

            var result = new DeleteEntityAction(Action.TargetID).Evaluate(Simulating);
            if (result is not Fragment.DeleteEntity.SucceedResult succeedResult)
            {
                return new DeleteEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(DeleteEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(DeleteEntityEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyEntityEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<DestroyEntityEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnDestroyEntity(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private DestroyEntityResult CheckRejection()
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
        private DestroyEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
