using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;
using RineaR.MadeHighlow.Actions.UnregisterTile;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public class DeleteTileEvaluator
    {
        public DeleteTileEvaluator(
            [NotNull] ActionContext context,
            [NotNull] IHistory initial,
            [NotNull] DeleteTileAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DeleteTileAction Action { get; }

        [CanBeNull] private Tile Target { get; set; }
        [CanBeNull] private ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents { get; set; }
        [CanBeNull] private Event<UnregisterTile.SucceedResult> UnregisterTileEvent { get; set; }
        [CanBeNull] private DeleteTileProcess Process { get; set; }

        [NotNull]
        public DeleteTileResult Evaluate()
        {
            DeleteTileResult result;

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
        private DeleteTileResult FindTarget()
        {
            Target = Context.Finder.FindTile(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteTileResult CheckEntityRemaining()
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
        private DeleteTileResult DeleteComponents()
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

        private DeleteTileResult Unregister()
        {
            var result = Context.Actions.UnregisterTile(Simulating, new UnregisterTileAction(Action.TargetID));
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
            Process = new DeleteTileProcess(DeleteComponentEvents, UnregisterTileEvent);
        }

        [NotNull]
        private DeleteTileResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
