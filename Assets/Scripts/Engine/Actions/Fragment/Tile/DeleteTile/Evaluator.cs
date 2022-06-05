using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, [NotNull] Action action)
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
            if (IsEntityRemaining(target)) return Result;

            DeleteComponents(target);

            if (Result.DeleteComponents.Any(@event => @event.Content.Deleted == null)) return Result;

            Confirm();

            return Result;
        }

        [CanBeNull]
        private Tile FindTarget()
        {
            return Context.Finder.FindTile(Initial.World, Action.TargetID);
        }

        private bool IsEntityRemaining([NotNull] Tile target)
        {
            return !Context.Finder.SearchEntities(Simulating.World, new EntityCondition(target.Position2D)).IsEmpty;
        }

        private void DeleteComponents([NotNull] Tile target)
        {
            Result = Result with { DeleteComponents = ValueList<Event<DeleteComponent.Result>>.Empty };
            foreach (var component in target.Components)
            {
                var action = new DeleteComponent.Action(component.ComponentID);
                var result = Context.Actions.DeleteComponent(Simulating, action);
                Simulating = Simulating.Appended(result, out var @event);
                Result = Result with { DeleteComponents = Result.DeleteComponents.Add(@event) };
            }
        }

        private void Confirm()
        {
            Result = Result with { Deleted = Result.Action.TargetID };
        }
    }
}
