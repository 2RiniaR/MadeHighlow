using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteTile
{
    public class DeleteTileEvaluator
    {
        public DeleteTileEvaluator([NotNull] IHistory initial, [NotNull] DeleteTileAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures((Contract.Result<DeleteTileResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteTileResult DeleteComponents()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(DeleteComponentEvents != null);

            DeleteComponentEvents = ValueList<Event<DeleteComponent.SucceedResult>>.Empty;

            foreach (var component in Target.Components)
            {
                var result = new DeleteComponentAction(component.ComponentID).Evaluate(Simulating);

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
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Ensures(UnregisterTileEvent != null);

            var result = new UnregisterTileAction(Action.TargetID).Evaluate(Simulating);
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
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Requires<InvalidOperationException>(UnregisterTileEvent != null);
            Contract.Ensures(Process != null);

            Process = new DeleteTileProcess(DeleteComponentEvents, UnregisterTileEvent);
        }

        [NotNull]
        private DeleteTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
