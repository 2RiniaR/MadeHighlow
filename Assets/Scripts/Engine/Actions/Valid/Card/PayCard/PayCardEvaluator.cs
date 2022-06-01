using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteCard;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public class PayCardEvaluator
    {
        public PayCardEvaluator([NotNull] ActionContext context, [NotNull] IHistory initial, PayCardAction action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private PayCardAction Action { get; }

        [CanBeNull] private Event<DeleteCard.SucceedResult> DeleteCardEvent { get; set; }
        [CanBeNull] private PayCardProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<PayCardExemption>> ExemptionInterrupts { get; set; }

        [NotNull]
        public PayCardResult Evaluate()
        {
            PayCardResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        private PayCardResult DeleteTarget()
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
            Process = new PayCardProcess(DeleteCardEvent);
        }

        [CanBeNull]
        private PayCardResult CheckRejection()
        {
            var effectors = Component.GetAllOfTypeFrom<IPayCardExempter>(Initial.World).Sort();

            ExemptionInterrupts = ValueList<Interrupt<PayCardExemption>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.PayCardExemption(Simulating, Action, Process, ExemptionInterrupts);
                if (interrupt == null) continue;
                ExemptionInterrupts = ExemptionInterrupts.Add(interrupt);
            }

            if (!ExemptionInterrupts.IsEmpty)
            {
                return new ExemptedResult(Action, Process, ExemptionInterrupts, ExemptionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private PayCardResult Succeed()
        {
            return new SucceedResult(Action, Process, ExemptionInterrupts);
        }
    }
}
