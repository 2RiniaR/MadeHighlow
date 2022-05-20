using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public class ElevateTileEvaluator
    {
        public ElevateTileEvaluator([NotNull] IHistory initial, ElevateTileAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private ElevateTileAction Action { get; }

        [CanBeNull] private Tile Target { get; set; }

        [CanBeNull] private ValueList<Interrupt<ElevateTileRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public ElevateTileResult Evaluate()
        {
            ElevateTileResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private ElevateTileResult FindTarget()
        {
            Contract.Ensures((Contract.Result<ElevateTileResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private ElevateTileResult CheckRejection()
        {
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IElevateTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<ElevateTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.ElevateTileRejection(Initial, Action, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private ElevateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
