using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;
using RineaR.MadeHighlow.Actions.UnregisterCard;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public class DeleteCardEvaluator
    {
        public DeleteCardEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] DeleteCardAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
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
            Target = Context.Finder.FindCard(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteCardResult DeleteComponents()
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

        private DeleteCardResult Unregister()
        {
            var result = Context.Actions.UnregisterCard(Simulating, new UnregisterCardAction(Action.TargetID));
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
            Process = new DeleteCardProcess(DeleteComponentEvents, UnregisterCardEvent);
        }

        [NotNull]
        private DeleteCardResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
