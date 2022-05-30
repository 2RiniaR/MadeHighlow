using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public class EntityFlyEvaluator
    {
        public EntityFlyEvaluator([NotNull] IHistory initial, EntityFlyAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityFlyAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Event<MoveEntity.SucceedResult> MoveEntityEvent { get; set; }
        [CanBeNull] private EntityFlyProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityFlyResult Evaluate()
        {
            EntityFlyResult result;
/*
            result = FindTarget();
            if (result != null) return result;

            result = MoveShift();
            if (result != null) return result;

            result = MoveFall();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;
*/
            return Succeed();
        }
/*
        [CanBeNull]
        private EntityFlyResult FindTarget()
        {
            Contract.Ensures((Contract.Result<EntityFlyResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityFlyResult MoveShift()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(
                (Contract.Result<EntityFlyResult>() != null) ^ (ShiftMoveEvent != null && DestinationElevation != null)
            );

            var result = new MoveEntityAction(Action.TargetID, Action.Direction.To3D).Evaluate(Simulating);
            if (result is not MoveEntity.SucceedResult succeedResult)
            {
                return new ShiftFailedResult(Action, ClimbMoveEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            ShiftMoveEvent = succeedEvent;
            SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;

            return null;
        }

        [CanBeNull]
        private EntityFlyResult MoveFall()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbMoveEvents != null);
            Contract.Requires<InvalidOperationException>(ShiftMoveEvent != null);

            var distance = new Distance(SteppingTarget.Position3D.Z.Value - DestinationElevation.Height.Value);
            FallMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZPositive).Evaluate(Simulating);
                if (result is not MoveEntity.SucceedResult succeedResult)
                {
                    return new FallFailedResult(Action, ClimbMoveEvents, ShiftMoveEvent, FallMoveEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                FallMoveEvents = FallMoveEvents.Add(succeedEvent);
                SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;
            }

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(MoveEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new EntityFlyProcess(MoveEntityEvent);
        }

        [CanBeNull]
        private EntityFlyResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityFlyRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<EntityFlyRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.EntityFlyRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }
*/
        [NotNull]
        private EntityFlyResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
