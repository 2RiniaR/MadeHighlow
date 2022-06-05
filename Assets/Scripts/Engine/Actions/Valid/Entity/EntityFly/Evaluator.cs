using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
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
            Result result;

            result = FindTarget();
            if (result != null) return result;

            result = FollowRoute();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindTarget()
        {
            Target = Context.Finder.FindEntity(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result FollowRoute()
        {
            FollowMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var result = Context.Actions.MoveEntity(
                    Simulating,
                    new MoveEntity.Action(Action.TargetID, step.Direction)
                );
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
        private ValueList<Event<MoveEntity.SucceedResult>> Fall([NotNull] EntityID entityID, [NotNull] IHistory initial)
        {
            var target = Context.Finder.FindEntity(initial.World, entityID) ??
                         throw new InvalidOperationException("この時点で必ず、移動対象のエンティティが存在するはず。");

            if (target.Levitation)
            {
                return ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            }

            var destinationTile = target.Position3D.To2D().GetTile(initial.World) ??
                                  throw new InvalidOperationException("この時点で必ず、落下先のタイルが存在するはず。");
            var destinationElevation = destinationTile.Elevation as GroundElevation ??
                                       throw new InvalidOperationException("この時点で必ず、落下先のタイルは着地可能なはず。");
            var distance = new Distance(target.Position3D.Z.Value - destinationElevation.Height.Value);

            var simulating = initial;
            var events = ValueList<Event<MoveEntity.SucceedResult>>.Empty;
            for (var i = 0; i < distance.Value; i++)
            {
                var result = Context.Actions.MoveEntity(
                    initial,
                    new MoveEntity.Action(entityID, Direction3D.ZNegative)
                );
                if (result is not MoveEntity.SucceedResult succeedResult) return null;

                simulating = simulating.Appended(succeedResult, out var succeedEvent);
                events = events.Add(succeedEvent);
            }

            return events;
        }

        private void WrapProcess()
        {
            Process = new Process(FollowMoveEvents, FallMoveEvents);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
