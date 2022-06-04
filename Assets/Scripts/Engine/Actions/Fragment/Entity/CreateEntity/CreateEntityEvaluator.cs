using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public class CreateEntityEvaluator
    {
        public CreateEntityEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            CreateEntityAction action
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
        [NotNull] private CreateEntityAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }

        [CanBeNull] private ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents { get; set; }

        [CanBeNull] private Event<RegisterEntityResult> RegisterEntityEvent { get; set; }
        [CanBeNull] private CreateEntityProcess Process { get; set; }

        [NotNull]
        public CreateEntityResult Evaluate()
        {
            CreateEntityResult result;

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
            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreateEntityResult Register()
        {
            var result = Context.Actions.RegisterEntity(
                Simulating,
                new RegisterEntityAction(AllocateIDEvent.Result.AllocatedID, Action.InitialProps)
            );

            Simulating = Simulating.Appended(result, out var @event);
            RegisterEntityEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreateEntityResult CreateComponents()
        {
            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = Context.Actions.CreateComponent(
                    Simulating,
                    new CreateComponentAction(RegisterEntityEvent.Result.Registered.EntityID, component)
                );

                var succeedResult = result as CreateComponent.SucceedResult;
                if (succeedResult == null)
                {
                    return new CreateComponentFailedResult(Action, RegisterEntityEvent, CreateComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                CreateComponentEvents = CreateComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Process = new CreateEntityProcess(AllocateIDEvent, RegisterEntityEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreateEntityResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
