using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;
using RineaR.MadeHighlow.Actions.UnregisterEntity;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public class DeleteEntityEvaluator
    {
        public DeleteEntityEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] DeleteEntityAction action
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
        [NotNull] private DeleteEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents { get; set; }
        [CanBeNull] private Event<UnregisterEntity.SucceedResult> UnregisterEntityEvent { get; set; }
        [CanBeNull] private DeleteEntityProcess Process { get; set; }

        [NotNull]
        public DeleteEntityResult Evaluate()
        {
            DeleteEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            result = DeleteComponents();
            if (result != null) return result;

            result = Unregister();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private DeleteEntityResult FindTarget()
        {
            Target = Context.Finder.FindEntity(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteEntityResult DeleteComponents()
        {
            DeleteComponentEvents = ValueList<Event<DeleteComponent.SucceedResult>>.Empty;

            foreach (var component in Target.Components)
            {
                var result = Context.Actions.DeleteComponent(
                    Simulating,
                    new DeleteComponentAction(component.ComponentID)
                );

                var succeedResult = result as DeleteComponent.SucceedResult;
                if (succeedResult == null)
                {
                    return new DeleteComponentFailedResult(Action, DeleteComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                DeleteComponentEvents = DeleteComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private DeleteEntityResult Unregister()
        {
            var result = Context.Actions.UnregisterEntity(Simulating, new UnregisterEntityAction(Action.TargetID));
            if (result is not UnregisterEntity.SucceedResult succeedResult)
            {
                return new UnregisterEntityFailedResult(Action, DeleteComponentEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            UnregisterEntityEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new DeleteEntityProcess(DeleteComponentEvents, UnregisterEntityEvent);
        }

        [NotNull]
        private DeleteEntityResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
