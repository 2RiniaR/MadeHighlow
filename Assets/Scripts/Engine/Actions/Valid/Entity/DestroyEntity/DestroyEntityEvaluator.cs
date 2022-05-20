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
        [CanBeNull] private DestroyEntityProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyEntityRejection>> RejectionInterrupts { get; set; }

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

            Process = new DestroyEntityProcess(DeleteEntityEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DestroyEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.DestroyEntityRejection(Simulating, Action, Process, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }
        }

        [CanBeNull]
        private DestroyEntityResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private DestroyEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
