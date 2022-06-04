using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public class GenerateEntityEvaluator
    {
        public GenerateEntityEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            GenerateEntityAction action
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
        [NotNull] private GenerateEntityAction Action { get; }

        [CanBeNull] private Event<CreateEntity.SucceedResult> CreateEntityEvent { get; set; }
        [CanBeNull] private GenerateEntityProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateEntityRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public GenerateEntityResult Evaluate()
        {
            GenerateEntityResult result;

            result = CreateTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private GenerateEntityResult CreateTarget()
        {
            var result = Context.Actions.CreateEntity(Simulating, new CreateEntityAction(Action.InitialProps));
            if (result is not CreateEntity.SucceedResult succeedResult)
            {
                return new CreateEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new GenerateEntityProcess(CreateEntityEvent);
        }

        [CanBeNull]
        private GenerateEntityResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IGenerateEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<GenerateEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.GenerateEntityRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private GenerateEntityResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
