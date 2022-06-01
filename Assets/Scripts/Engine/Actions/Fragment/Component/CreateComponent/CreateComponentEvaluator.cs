using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public class CreateComponentEvaluator
    {
        public CreateComponentEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            CreateComponentAction action
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
        [NotNull] private CreateComponentAction Action { get; }

        [CanBeNull] private Event<AllocateIDResult> AllocateIDEvent { get; set; }
        [CanBeNull] private Event<RegisterComponent.SucceedResult> RegisterComponentEvent { get; set; }
        [CanBeNull] private CreateComponentProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<CreateComponentRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public CreateComponentResult Evaluate()
        {
            CreateComponentResult result;

            AllocateID();

            result = Register();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        private void AllocateID()
        {
            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            AllocateIDEvent = @event;
        }

        [CanBeNull]
        private CreateComponentResult Register()
        {
            var result = Context.Actions.RegisterComponent(
                Simulating,
                new RegisterComponentAction(Action.TargetID, AllocateIDEvent.Result.AllocatedID, Action.InitialStatus)
            );

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
            Process = new CreateComponentProcess(AllocateIDEvent, RegisterComponentEvent);
        }

        [CanBeNull]
        private CreateComponentResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<ICreateComponentRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<CreateComponentRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.CreateComponentRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private CreateComponentResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
