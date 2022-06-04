using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterTile;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public class CreateTileEvaluator
    {
        public CreateTileEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            CreateTileAction action
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
        [NotNull] private CreateTileAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }

        [CanBeNull] private ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents { get; set; }

        [CanBeNull] private Event<RegisterTileResult> RegisterTileEvent { get; set; }
        [CanBeNull] private CreateTileProcess Process { get; set; }

        [NotNull]
        public CreateTileResult Evaluate()
        {
            CreateTileResult result;

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
        private CreateTileResult Register()
        {
            var result = Context.Actions.RegisterTile(
                Simulating,
                new RegisterTileAction(AllocateIDEvent.Result.AllocatedID, Action.InitialProps)
            );

            Simulating = Simulating.Appended(result, out var @event);
            RegisterTileEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreateTileResult CreateComponents()
        {
            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = Context.Actions.CreateComponent(
                    Simulating,
                    new CreateComponentAction(RegisterTileEvent.Result.Registered.TileID, component)
                );

                var succeedResult = result as CreateComponent.SucceedResult;
                if (succeedResult == null)
                {
                    return new CreateComponentFailedResult(Action, RegisterTileEvent, CreateComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                CreateComponentEvents = CreateComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        private void WrapProcess()
        {
            Process = new CreateTileProcess(AllocateIDEvent, RegisterTileEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreateTileResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
