using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public class EntityTeleportEvaluator
    {
        public EntityTeleportEvaluator([NotNull] IHistory initial, EntityTeleportAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityTeleportAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Event<Fragment.PositionEntity.SucceedResult> PositionEntityEvent { get; set; }
        [CanBeNull] private EntityTeleportProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityTeleportRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityTeleportResult Evaluate()
        {
            EntityTeleportResult result;

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
        private EntityTeleportResult FindTarget()
        {
            Contract.Ensures((Contract.Result<EntityTeleportResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityTeleportResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityTeleportResult>() != null) ^ (PositionEntityEvent != null));

            var result = new PositionEntityAction(Action.TargetID, Action.Destination).Evaluate(Simulating);
            if (result is not Fragment.PositionEntity.SucceedResult succeedResult)
            {
                return new PositionEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PositionEntityEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(PositionEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new EntityTeleportProcess(PositionEntityEvent);
        }

        [CanBeNull]
        private EntityTeleportResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityTeleportRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<EntityTeleportRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.EntityTeleportRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private EntityTeleportResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
