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
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Tile Target { get; set; }
        [CanBeNull] private ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents { get; set; }
        [CanBeNull] private Event<UnregisterTile.SucceedResult> UnregisterTileEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckEntityRemaining();
            if (result != null) return result;

            result = DeleteComponents();
            if (result != null) return result;

            result = Unregister();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private Result FindTarget()
        {
            Target = Context.Finder.FindTile(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result CheckEntityRemaining()
        {
            var removable = Context.Finder.SearchEntities(Simulating.World, new EntityCondition(Target.Position2D))
                .IsEmpty;
            if (!removable)
            {
                return new EntityRemainingResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result DeleteComponents()
        {
            DeleteComponentEvents = ValueList<Event<DeleteComponent.SucceedResult>>.Empty;

            foreach (var component in Target.Components)
            {
                var result = Context.Actions.DeleteComponent(
                    Simulating,
                    new DeleteComponent.Action(component.ComponentID)
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

        private Result Unregister()
        {
            var result = Context.Actions.UnregisterTile(Simulating, new UnregisterTile.Action(Action.TargetID));
            if (result is not UnregisterTile.SucceedResult succeedResult)
            {
                return new UnregisterTileFailedResult(Action, DeleteComponentEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            UnregisterTileEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(DeleteComponentEvents, UnregisterTileEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
