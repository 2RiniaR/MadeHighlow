using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteTile;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public class DestroyTileEvaluator
    {
        public DestroyTileEvaluator([NotNull] IHistory initial, DestroyTileAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DestroyTileAction Action { get; }

        [CanBeNull] private Event<Fragment.DeleteTile.SucceedResult> DeleteTileEvent { get; set; }
        [CanBeNull] private DestroyTileProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyTileRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DestroyTileResult Evaluate()
        {
            DestroyTileResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyTileResult DeleteTarget()
        {
            Contract.Ensures((Contract.Result<DestroyTileResult>() != null) ^ (DeleteTileEvent != null));

            var result = new DeleteTileAction(Action.TargetID).Evaluate(Simulating);
            if (result is not Fragment.DeleteTile.SucceedResult succeedResult)
            {
                return new DeleteTileFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteTileEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(DeleteTileEvent != null);
            Contract.Ensures(Process != null);

            Process = new DestroyTileProcess(DeleteTileEvent);
        }

        [CanBeNull]
        private DestroyTileResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DestroyTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.DestroyTileRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private DestroyTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
