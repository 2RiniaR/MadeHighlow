using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteEntity;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public class DestroyEntityEvaluator
    {
        public DestroyEntityEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            DestroyEntityAction action
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
        [NotNull] private DestroyEntityAction Action { get; }

        [CanBeNull] private Event<DeleteEntity.SucceedResult> DeleteEntityEvent { get; set; }
        [CanBeNull] private DestroyEntityProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyEntityRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DestroyEntityResult Evaluate()
        {
            DestroyEntityResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyEntityResult DeleteTarget()
        {
            var result = Context.Actions.DeleteEntity(Simulating, new DeleteEntityAction(Action.TargetID));
            if (result is not DeleteEntity.SucceedResult succeedResult)
            {
                return new DeleteEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new DestroyEntityProcess(DeleteEntityEvent);
        }

        [CanBeNull]
        private DestroyEntityResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IDestroyEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DestroyEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.DestroyEntityRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private DestroyEntityResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
