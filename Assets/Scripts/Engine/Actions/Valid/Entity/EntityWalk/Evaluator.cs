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
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var actor = FindActor();

            if (actor == null) return Result;

            FollowRoute();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result with { IsConfirmed = true };
        }

        [CanBeNull]
        private Entity FindActor()
        {
            return Context.Finder.FindEntity(Simulating.World, Action.ActorID);
        }

        private void FollowRoute()
        {
            var events = ValueList<Event<ReactedResult<EntityStep.Result>>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var action = new EntityStep.Action(Action.ActorID, step.Direction, Action.Available);
                var result = Context.Actions.EntityStep(Simulating, action);

                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { EntitySteps = events };
        }
    }
}
