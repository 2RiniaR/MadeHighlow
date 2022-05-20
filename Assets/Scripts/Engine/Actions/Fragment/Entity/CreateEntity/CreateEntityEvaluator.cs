using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateEntity
{
    public class CreateEntityEvaluator
    {
        public CreateEntityEvaluator([NotNull] IHistory initial, CreateEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures(AllocateIDEvent != null);

            var result = new AllocateIDAction().Evaluate(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreateEntityResult Register()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Ensures((Contract.Result<CreateEntityResult>() != null) ^ (RegisterEntityEvent != null));

            var result = new RegisterEntityAction(
                AllocateIDEvent.Result.AllocatedID,
                Action.InitialProps
            ).Evaluate(Simulating);

            Simulating = Simulating.Appended(result, out var @event);
            RegisterEntityEvent = @event;

            return null;
        }

        [CanBeNull]
        private CreateEntityResult CreateComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityEvent != null);
            Contract.Ensures(CreateComponentEvents != null);

            CreateComponentEvents = ValueList<Event<CreateComponent.SucceedResult>>.Empty;

            foreach (var component in Action.InitialProps.Components)
            {
                var result = new CreateComponentAction(RegisterEntityEvent.Result.Registered.EntityID, component)
                    .Evaluate(Simulating);

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
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Requires<InvalidOperationException>(RegisterEntityEvent != null);
            Contract.Requires<InvalidOperationException>(CreateComponentEvents != null);
            Contract.Ensures(Process != null);

            Process = new CreateEntityProcess(AllocateIDEvent, RegisterEntityEvent, CreateComponentEvents);
        }

        [NotNull]
        private CreateEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
