using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterCard;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public class DeleteCardEvaluator
    {
        public DeleteCardEvaluator([NotNull] IHistory initial, [NotNull] DeleteCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private DeleteCardAction Action { get; }

        [CanBeNull] private Card Target { get; set; }
        [CanBeNull] private ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents { get; set; }
        [CanBeNull] private Event<UnregisterCard.SucceedResult> UnregisterCardEvent { get; set; }
        [CanBeNull] private DeleteCardProcess Process { get; set; }

        [NotNull]
        public DeleteCardResult Evaluate()
        {
            DeleteCardResult result;

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
        private DeleteCardResult FindTarget()
        {
            Contract.Ensures((Contract.Result<DeleteCardResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteCardResult DeleteComponents()
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

        private DeleteCardResult Unregister()
        {
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Ensures(UnregisterCardEvent != null);

            var result = new UnregisterCardAction(Action.TargetID).Evaluate(Simulating);
            if (result is not UnregisterCard.SucceedResult succeedResult)
            {
                return new UnregisterCardFailedResult(Action, DeleteComponentEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            UnregisterCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(DeleteComponentEvents != null);
            Contract.Requires<InvalidOperationException>(UnregisterCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new DeleteCardProcess(DeleteComponentEvents, UnregisterCardEvent);
        }

        [NotNull]
        private DeleteCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
