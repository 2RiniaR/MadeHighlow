using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
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

        [NotNull] private static readonly Cost ClimbCost = new(5);
        [NotNull] private static readonly Cost HorizontalCost = new(1);
        [NotNull] private static readonly Cost FallCost = new(0);

        [NotNull]
        public Result Evaluate()
        {
            var actor = FindActor();

            if (actor == null) return Result;

            var tile = FindDestinationTile(actor);

            if (tile == null || tile.Elevation is not GroundElevation ground || ground.Placeable == false)
                return Result;

            MoveClimb(actor, ground);
            actor = Result.ClimbDistance.Value == 0 ? actor : Result.ClimbMoves.Last().Content.Moved;

            if (actor == null) return Result;

            MoveShift();
            actor = Result.ShiftMove.Content.Moved;

            if (actor == null) return Result;

            MoveFall(actor, ground);
            actor = Result.FallDistance.Value == 0 ? actor : Result.FallMoves.Last().Content.Moved;

            if (actor == null) return Result;

            CheckCostIsEnough();

            if (Action.Available < Result.Expended) return Result;

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

        [CanBeNull]
        private Tile FindDestinationTile([NotNull] Entity actor)
        {
            var position = actor.Position3D.To2D().MoveTo(Action.Direction, new Distance(1));
            return Context.Finder.FindTile(Simulating.World, position);
        }

        private void MoveClimb([NotNull] Entity actor, [NotNull] GroundElevation ground)
        {
            Result = Result with { ClimbDistance = new Distance(ground.Height.Value - actor.Position3D.Z.Value) };

            var events = ValueList<Event<MoveEntity.Result>>.Empty;
            for (var i = 0; i < Result.ClimbDistance.Value; i++)
            {
                var action = new MoveEntity.Action(Action.ActorID, Direction3D.ZPositive);
                var result = Context.Actions.MoveEntity(Simulating, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);

                if (result.Moved == null) break;
            }

            Result = Result with { ClimbMoves = events };
        }

        private void MoveShift()
        {
            var action = new MoveEntity.Action(Action.ActorID, Action.Direction.To3D);
            var result = Context.Actions.MoveEntity(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { ShiftMove = @event };
        }

        private void MoveFall([NotNull] Entity actor, [NotNull] GroundElevation ground)
        {
            Result = Result with { FallDistance = new Distance(actor.Position3D.Z.Value - ground.Height.Value) };

            var events = ValueList<Event<MoveEntity.Result>>.Empty;
            for (var i = 0; i < Result.FallDistance.Value; i++)
            {
                var action = new MoveEntity.Action(Action.ActorID, Direction3D.ZNegative);
                var result = Context.Actions.MoveEntity(Simulating, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);

                if (result.Moved == null) break;
            }

            Result = Result with { FallMoves = events };
        }

        [NotNull]
        private Cost BaseCost()
        {
            return ClimbCost * Result.ClimbDistance.Value + HorizontalCost + FallCost * Result.FallDistance.Value;
        }

        private void CheckCostIsEnough()
        {
            var effectors = Context.Finder.GetAllComponents<ICostEffector>(Initial.World).Sort();

            var costEffects = ValueList<Interrupt<CostEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var context = new CostEffectContext(Initial, Result, costEffects);
                var interrupts = effector.CostEffects(context);
                if (interrupts == null) continue;
                costEffects = costEffects.AddRange(interrupts);
            }

            costEffects = costEffects.Sort();

            var cost = BaseCost();
            foreach (var costEffect in costEffects)
            {
                if (costEffect.Content.AdditionValue != null)
                {
                    cost += costEffect.Content.AdditionValue.Value;
                    continue;
                }

                if (costEffect.Content.ReductionValue != null)
                {
                    cost -= costEffect.Content.ReductionValue.Value;
                    continue;
                }

                if (costEffect.Content.OverwriteValue != null)
                {
                    cost = costEffect.Content.OverwriteValue;
                }
            }

            Result = Result with
            {
                CostEffects = costEffects,
                Expended = cost,
            };
        }
    }
}
