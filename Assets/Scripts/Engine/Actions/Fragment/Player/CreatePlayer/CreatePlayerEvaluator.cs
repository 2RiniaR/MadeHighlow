using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class CreatePlayerEvaluator
    {
        public CreatePlayerEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            CreatePlayerAction action
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
        [NotNull] private CreatePlayerAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }

        [CanBeNull] private ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents { get; set; }

        [CanBeNull] private Event<RegisterPlayerResult> RegisterPlayerEvent { get; set; }
        [CanBeNull] private CreatePlayerProcess Process { get; set; }

        [NotNull]
        public CreatePlayerResult Evaluate()
        {
            CreatePlayerResult result;

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
        private CreatePlayerResult Register()
        {
            var result = Context.Actions.RegisterPlayer(
                Simulating,
                new RegisterPlayerAction(AllocateIDEvent.Result.AllocatedID, Action.InitialProps)
            );

            Simulating = Simulating.Appended(result, out var @event);
            RegisterPlayerEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreatePlayerResult CreateComponents()
        {
            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = Context.Actions.CreateComponent(
                    Simulating,
                    new CreateComponentAction(RegisterPlayerEvent.Result.Registered.PlayerID, component)
                );

                var succeedResult = result as CreateComponent.SucceedResult;
                if (succeedResult == null)
                {
                    return new CreateComponentFailedResult(Action, RegisterPlayerEvent, CreateComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                CreateComponentEvents = CreateComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Process = new CreatePlayerProcess(AllocateIDEvent, RegisterPlayerEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreatePlayerResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
