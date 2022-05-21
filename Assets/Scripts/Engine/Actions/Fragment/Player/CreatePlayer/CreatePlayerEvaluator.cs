using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.AllocateID;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.Fragment.CreatePlayer
{
    public class CreatePlayerEvaluator
    {
        public CreatePlayerEvaluator([NotNull] IHistory initial, CreatePlayerAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures(AllocateIDEvent != null);

            var result = new AllocateIDAction().Evaluate(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreatePlayerResult Register()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Ensures((Contract.Result<CreatePlayerResult>() != null) ^ (RegisterPlayerEvent != null));

            var result = new RegisterPlayerAction(
                AllocateIDEvent.Result.AllocatedID,
                Action.InitialProps
            ).Evaluate(Simulating);

            Simulating = Simulating.Appended(result, out var @event);
            RegisterPlayerEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreatePlayerResult CreateComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerEvent != null);
            Contract.Ensures(CreateComponentEvents != null);

            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = new CreateComponentAction(RegisterPlayerEvent.Result.Registered.PlayerID, component)
                    .Evaluate(Simulating);

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
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerEvent != null);
            Contract.Requires<InvalidOperationException>(CreateComponentEvents != null);
            Contract.Ensures(Process != null);

            Process = new CreatePlayerProcess(AllocateIDEvent, RegisterPlayerEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreatePlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
