using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
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

        private Event<AllocateID.Result> AllocateIDEvent { get; set; }
        private ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents { get; set; }
        private Event<RegisterCard.SucceedResult> RegisterCardEvent { get; set; }
        private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            AllocateID();

            result = Register();
            if (result != null) return result;

            result = CreateComponents();
            if (result != null) return result;

            Process = new Process(AllocateIDEvent, RegisterCardEvent, CreateComponentEvents);
            return new SucceedResult(Action, Process);
        }

        private void AllocateID()
        {
            Contract.Ensures(AllocateIDEvent != null);

            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private Result Register()
        {
            var result = Context.Actions.RegisterCard(
                Simulating,
                new RegisterCard.Action(Action.ParentID, AllocateIDEvent.Result.AllocatedID, Action.InitialProps)
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
        private Result CreateComponents()
        {
            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = Context.Actions.CreateComponent(
                    Simulating,
                    new CreateComponent.Action(RegisterCardEvent.Result.Registered.CardID, component)
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
    }
}
