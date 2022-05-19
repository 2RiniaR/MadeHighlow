using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteEntity
{
    public class DeleteEntityEvaluator
    {
        public DeleteEntityEvaluator([NotNull] IHistory initial, [NotNull] DeleteEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DeleteEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents { get; set; }
        [CanBeNull] private Event<UnregisterEntity.SucceedResult> UnregisterEntityEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

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
            Contract.Ensures((Contract.Result<DeleteEntityResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteEntityResult DeleteComponents()
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

        private DeleteEntityResult Unregister()
        {
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Ensures(UnregisterEntityEvent != null);

            var result = new UnregisterEntityAction(Action.TargetID).Evaluate(Simulating);
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
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Requires<InvalidOperationException>(UnregisterEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(DeleteComponentEvents, UnregisterEntityEvent);
        }

        [NotNull]
        private DeleteEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
