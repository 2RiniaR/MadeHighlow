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
            var target = FindTarget();

            if (target == null) return Result;

            FollowRoute();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result;
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private void FollowRoute()
        {
            var followMoves = ValueList<Event<MoveEntity.Result>>.Empty;
            var prevFallMoves = ValueList<Event<MoveEntity.Result>>.Empty;
            foreach (var step in Action.Route.Steps)
            {
                var action = new MoveEntity.Action(Action.TargetID, step.Direction);
                var result = Context.Actions.MoveEntity(Simulating, action);
                var moved = Simulating.Appended(result, out var @event);

                var fallMoves = Fall(Action.TargetID, moved);
                if (fallMoves == null) break;

                Simulating = moved;
                followMoves = followMoves.Add(@event);
                prevFallMoves = fallMoves;
            }

            Result = Result with
            {
                FallMoves = prevFallMoves,
                FollowMoves = followMoves,
            };
        }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<MoveEntity.Result>> Fall([NotNull] EntityID entityID, [NotNull] IHistory initial)
        {
            var target = Context.Finder.FindEntity(initial.World, entityID) ??
                         throw new InvalidOperationException("この時点で必ず、移動対象のエンティティが存在するはず。");

            if (target.Levitation) return ValueList<Event<MoveEntity.Result>>.Empty;

            var destinationTile = Context.Finder.FindTile(initial.World, target.Position3D.To2D()) ??
                                  throw new InvalidOperationException("この時点で必ず、落下先のタイルが存在するはず。");

            var destinationElevation = destinationTile.Elevation as GroundElevation ??
                                       throw new InvalidOperationException("この時点で必ず、落下先のタイルは着地可能なはず。");

            var distance = new Distance(target.Position3D.Z.Value - destinationElevation.Height.Value);

            var simulating = initial;
            var events = ValueList<Event<MoveEntity.Result>>.Empty;
            for (var i = 0; i < distance.Value; i++)
            {
                var action = new MoveEntity.Action(entityID, Direction3D.ZNegative);
                var result = Context.Actions.MoveEntity(initial, action);
                simulating = simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            return events;
        }
    }
}
