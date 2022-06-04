using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Event<ReactedResult<EntityStep.SucceedResult>>> EntityStepEvents { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindActor();
            if (result != null) return result;

            result = FollowRoute();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindActor()
        {
            Target = Context.Finder.FindEntity(Simulating.World, Action.ActorID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result FollowRoute()
        {
            EntityStepEvents = ValueList<Event<ReactedResult<EntityStep.SucceedResult>>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var result = Context.Actions.EntityStep(
                    Simulating,
                    new EntityStep.Action(Action.ActorID, step.Direction, Action.Available)
                );
                var succeedResult = result.BodyAs<EntityStep.SucceedResult>();
                if (succeedResult == null) break;

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                EntityStepEvents = EntityStepEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(EntityStepEvents);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
