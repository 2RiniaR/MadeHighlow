using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public class EntityWalkEvaluator
    {
        public EntityWalkEvaluator([NotNull] IHistory initial, EntityWalkAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityWalkAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Event<ReactedResult<EntityStep.SucceedResult>>> EntityStepEvents { get; set; }
        [CanBeNull] private EntityWalkProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityWalkRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityWalkResult Evaluate()
        {
            EntityWalkResult result;

            result = FindActor();
            if (result != null) return result;

            result = FollowRoute();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityWalkResult FindActor()
        {
            Contract.Ensures((Contract.Result<EntityWalkResult>() != null) ^ (Target != null));

            Target = Action.ActorID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityWalkResult FollowRoute()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityWalkResult>() != null) ^ (EntityStepEvents != null));

            EntityStepEvents = ValueList<Event<ReactedResult<EntityStep.SucceedResult>>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var result = new EntityStepAction(Action.ActorID, step.Direction, Action.Available)
                    .Evaluate(Simulating);
                var succeedResult = result.BodyAs<EntityStep.SucceedResult>();
                if (succeedResult == null) break;

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                EntityStepEvents = EntityStepEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(EntityStepEvents != null);
            Contract.Ensures(Process != null);

            Process = new EntityWalkProcess(EntityStepEvents);
        }

        [CanBeNull]
        private EntityWalkResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityWalkRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<EntityWalkRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.EntityWalkRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private EntityWalkResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
