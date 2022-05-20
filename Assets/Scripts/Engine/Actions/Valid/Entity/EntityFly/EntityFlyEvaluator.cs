using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
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
        [CanBeNull] private Event<Fragment.MoveEntity.SucceedResult> MoveEntityEvent { get; set; }
        [CanBeNull] private EntityFlyProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityFlyResult Evaluate()
        {
            EntityFlyResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckCanFly();
            if (result != null) return result;

            result = Move();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

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
        private EntityFlyResult CheckCanFly()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            if (Target.Levitation == false)
            {
                return new CanNotFlyResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityFlyResult Move()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityFlyResult>() != null) ^ (MoveEntityEvent != null));

            var result = new MoveEntityAction(Action.TargetID, Action.Direction).Evaluate(Simulating);
            if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
            {
                return new MoveEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            MoveEntityEvent = succeedEvent;

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
                var interrupts = effector.EntityFlyRejection(Simulating, Action, Process, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private EntityFlyResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
