using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public class MoveEntityEvaluator
    {
        public MoveEntityEvaluator([NotNull] IHistory initial, MoveEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private MoveEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<MoveEntityRejection>> RejectionInterrupts { get; set; }
        [CanBeNull] private Event<PositionEntity.SucceedResult> PositionEntityEvent { get; set; }
        [CanBeNull] private MoveEntityProcess Process { get; set; }

        [NotNull]
        public MoveEntityResult Evaluate()
        {
            MoveEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private MoveEntityResult FindTarget()
        {
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private MoveEntityResult Position()
        {
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (PositionEntityEvent != null));
            Contract.Requires<InvalidOperationException>(Target != null);

            var result = new PositionEntityAction(
                Action.TargetID,
                Target.Position3D.MoveTo(Action.Direction, new Distance(1))
            ).Evaluate(Simulating);
            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PositionEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(PositionEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new MoveEntityProcess(PositionEntityEvent);
        }

        [CanBeNull]
        private MoveEntityResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IMoveEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<MoveEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.MoveEntityRejection(Simulating, Action, Process, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private MoveEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
