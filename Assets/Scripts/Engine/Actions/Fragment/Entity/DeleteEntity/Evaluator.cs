using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
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

            DeleteComponents(target);

            if (Result.DeleteComponents.Any(@event => @event.Content.DeletedID == null)) return Result;

            Unregister();

            if (Result.UnregisterEntity.Content.UnregisteredID == null) return Result;

            Confirm();

            return Result;
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private void DeleteComponents([NotNull] Entity target)
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

        private void Unregister()
        {
            var action = new UnregisterEntity.Action(Action.TargetID);
            var result = Context.Actions.UnregisterEntity(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { UnregisterEntity = @event };
        }

        private void Confirm()
        {
            Result = Result with { DeletedID = Action.TargetID };
        }
    }
}
