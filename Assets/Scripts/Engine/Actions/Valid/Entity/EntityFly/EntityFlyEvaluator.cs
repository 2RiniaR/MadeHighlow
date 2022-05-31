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

        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> FollowMoveEvents { get; set; }
        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents { get; set; }
        [CanBeNull] private EntityFlyProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityFlyResult Evaluate()
        {
            EntityFlyResult result;

            result = FindTarget();
            if (result != null) return result;

            result = FollowRoute();
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
        private EntityFlyResult FollowRoute()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityFlyResult>() != null) ^ (FollowMoveEvents != null));

            FollowMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var result = new MoveEntityAction(Action.TargetID, step.Direction).Evaluate(Simulating);
                if (result is not MoveEntity.SucceedResult succeedResult)
                {
                    break;
                }

                var moved = Simulating.Appended(succeedResult, out var succeedEvent);

                var fallMoveEvents = Fall(Action.TargetID, moved);
                if (fallMoveEvents == null)
                {
                    break;
                }

                Simulating = moved;
                FollowMoveEvents = FollowMoveEvents!.Add(succeedEvent);
                FallMoveEvents = fallMoveEvents;
            }

            return null;
        }

        [CanBeNull]
        [ItemNotNull]
        private static ValueList<Event<MoveEntity.SucceedResult>> Fall(
            [NotNull] EntityID entityID,
            [NotNull] IHistory initial
        )
        {
            var target = entityID.GetFrom(initial.World);
            Contract.Requires<InvalidOperationException>(target != null, "この時点で必ず、移動対象のエンティティが存在するはず。");

            if (target.Levitation)
            {
                return ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            }

            var destinationTile = target.Position3D.To2D().GetTile(initial.World);
            Contract.Requires<InvalidOperationException>(destinationTile != null, "この時点で必ず、落下先のタイルが存在するはず。");

            var destinationElevation = destinationTile.Elevation as GroundElevation;
            Contract.Requires<InvalidOperationException>(destinationElevation != null, "この時点で必ず、落下先のタイルは着地可能なはず。");

            var distance = new Distance(target.Position3D.Z.Value - destinationElevation.Height.Value);

            var simulating = initial;
            var events = ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(entityID, Direction3D.ZNegative).Evaluate(initial);
                if (result is not MoveEntity.SucceedResult succeedResult) return null;

                simulating = simulating.Appended(succeedResult, out var succeedEvent);
                events = events.Add(succeedEvent);
            }

            return events;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(FollowMoveEvents != null);
            Contract.Requires<InvalidOperationException>(FallMoveEvents != null);
            Contract.Ensures(Process != null);

            Process = new EntityFlyProcess(FollowMoveEvents, FallMoveEvents);
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

        [NotNull]
        private EntityFlyResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
