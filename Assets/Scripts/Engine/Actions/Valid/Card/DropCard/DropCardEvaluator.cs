using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteCard;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public class DropCardEvaluator
    {
        public DropCardEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            DropCardAction action
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
        [NotNull] private DropCardAction Action { get; }

        [CanBeNull] private Event<DeleteCard.SucceedResult> DeleteCardEvent { get; set; }
        [CanBeNull] private DropCardProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<DropCardRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DropCardResult Evaluate()
        {
            DropCardResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        private DropCardResult DeleteTarget()
        {
            var result = Context.Actions.DeleteCard(Simulating, new DeleteCardAction(Action.TargetID));
            if (result is not DeleteCard.SucceedResult succeedResult)
            {
                return new DeleteCardFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new DropCardProcess(DeleteCardEvent);
        }

        [CanBeNull]
        private DropCardResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IDropCardRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DropCardRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.DropCardRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private DropCardResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
