using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public class CreateComponentEvaluator
    {
        public CreateComponentEvaluator([NotNull] IHistory initial, CreateComponentAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private CreateComponentAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }
        [CanBeNull] private Event<RegisterComponent.SucceedResult> RegisterComponentEvent { get; set; }
        [CanBeNull] private CreateComponentProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<CreateComponentEffect>> Interrupts { get; set; }

        [NotNull]
        public CreateComponentResult Evaluate()
        {
            CreateComponentResult result;

            AllocateID();

            result = Register();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

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
        private CreateComponentResult Register()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Ensures((Contract.Result<CreateComponentResult>() != null) ^ (RegisterComponentEvent != null));

            var result = new RegisterComponentAction(
                Action.TargetID,
                AllocateIDEvent.Result.AllocatedID,
                Action.InitialStatus
            ).Evaluate(Simulating);

            if (result is not RegisterComponent.SucceedResult succeedResult)
            {
                return new RegisterComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            RegisterComponentEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDEvent != null);
            Contract.Requires<InvalidOperationException>(RegisterComponentEvent != null);
            Contract.Ensures(Process != null);

            Process = new CreateComponentProcess(AllocateIDEvent, RegisterComponentEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<ICreateComponentEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<CreateComponentEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnCreateComponent(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private CreateComponentResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private CreateComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
