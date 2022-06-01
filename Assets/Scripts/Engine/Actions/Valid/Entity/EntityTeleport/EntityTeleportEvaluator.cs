using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PositionEntity;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public class EntityTeleportEvaluator
    {
        public EntityTeleportEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            EntityTeleportAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityTeleportAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Event<PositionEntity.SucceedResult> PositionEntityEvent { get; set; }
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
            Target = Context.Finder.FindEntity(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityTeleportResult Position()
        {
            var result = Context.Actions.PositionEntity(
                Simulating,
                new PositionEntityAction(Action.TargetID, Action.Destination)
            );
            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return new PositionEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PositionEntityEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new EntityTeleportProcess(PositionEntityEvent);
        }

        [CanBeNull]
        private EntityTeleportResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IEntityTeleportRejector>(Initial.World).Sort();

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
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
