using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterCard;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public class CreateCardEvaluator
    {
        public CreateCardEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            CreateCardAction action
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
        [NotNull] private CreateCardAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }

        [CanBeNull] private ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents { get; set; }

        [CanBeNull] private Event<RegisterCard.SucceedResult> RegisterCardEvent { get; set; }
        [CanBeNull] private CreateCardProcess Process { get; set; }

        [NotNull]
        public CreateCardResult Evaluate()
        {
            CreateCardResult result;

            AllocateID();

            result = Register();
            if (result != null) return result;

            result = CreateComponents();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        private void AllocateID()
        {
            Contract.Ensures(AllocateIDEvent != null);

            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreateCardResult Register()
        {
            var result = Context.Actions.RegisterCard(
                Simulating,
                new RegisterCardAction(Action.ParentID, AllocateIDEvent.Result.AllocatedID, Action.InitialProps)
            );

            if (result is not RegisterCard.SucceedResult succeedResult)
            {
                return new RegisterCardFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            RegisterCardEvent = succeedEvent;

            return null;
        }

        [CanBeNull]
        private CreateCardResult CreateComponents()
        {
            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = Context.Actions.CreateComponent(
                    Simulating,
                    new CreateComponentAction(RegisterCardEvent.Result.Registered.CardID, component)
                );

                var succeedResult = result as CreateComponent.SucceedResult;
                if (succeedResult == null)
                {
                    return new CreateComponentFailedResult(Action, RegisterCardEvent, CreateComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                CreateComponentEvents = CreateComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Process = new CreateCardProcess(AllocateIDEvent, RegisterCardEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreateCardResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
