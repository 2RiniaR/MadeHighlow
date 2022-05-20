using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateTile
{
    public class CreateTileEvaluator
    {
        public CreateTileEvaluator([NotNull] IHistory initial, CreateTileAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures(AllocateIDEvent != null);

            var result = new AllocateIDAction().Evaluate(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreateTileResult Register()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Ensures((Contract.Result<CreateTileResult>() != null) ^ (RegisterTileEvent != null));

            var result = new RegisterTileAction(
                AllocateIDEvent.Result.AllocatedID,
                Action.InitialProps
            ).Evaluate(Simulating);

            Simulating = Simulating.Appended(result, out var @event);
            RegisterTileEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreateTileResult CreateComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileEvent != null);
            Contract.Ensures(CreateComponentEvents != null);

            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = new CreateComponentAction(RegisterTileEvent.Result.Registered.TileID, component)
                    .Evaluate(Simulating);

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
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Requires<InvalidOperationException>(RegisterTileEvent != null);
            Contract.Requires<InvalidOperationException>(CreateComponentEvents != null);
            Contract.Ensures(Process != null);

            Process = new CreateTileProcess(AllocateIDEvent, RegisterTileEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
