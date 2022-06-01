using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityFly;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public class KnockBackEvaluator
    {
        public KnockBackEvaluator(
            [NotNull] ActionContext context,
            [NotNull] IHistory initial,
            [NotNull] KnockBackAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private KnockBackAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<KnockBackCalculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private KnockBack Calculated { get; set; }

        [CanBeNull] private Event<ReactedResult<EntityFly.SucceedResult>> EntityFlyEvent { get; set; }
        [CanBeNull] private KnockBackProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<KnockBackRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public KnockBackResult Evaluate()
        {
            KnockBackResult result;

            result = FindTarget();
            if (result != null) return result;

            CalculateKnockBack();

            result = Fly();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private KnockBackResult FindTarget()
        {
            Target = Context.Finder.FindEntity(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        private void CalculateKnockBack()
        {
            var effectors = Component.GetAllOfTypeFrom<IKnockBackCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<KnockBackCalculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.KnockBackCalculations(Initial, Action, CalculationInterrupts);
                if (interrupts == null) continue;
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }

            Calculated = Action.KnockBack;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Effect is Reduction reduction)
                {
                    Calculated = reduction.Value.Caused(Calculated);
                }
            }
        }

        [CanBeNull]
        private KnockBackResult Fly()
        {
            var steps = Enumerable.Range(0, Calculated.Distance.Value)
                .Select(_ => new EntityFlyStep(Calculated.Direction))
                .ToValueList();
            var result = Context.Actions.EntityFly(
                Simulating,
                new EntityFlyAction(Action.TargetID, new EntityFlyRoute(steps))
            );
            var succeedResult = result.BodyAs<EntityFly.SucceedResult>();
            if (succeedResult == null)
            {
                return new EntityFlyFailedResult(Action, CalculationInterrupts, Calculated, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            EntityFlyEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new KnockBackProcess(EntityFlyEvent);
        }

        [CanBeNull]
        private KnockBackResult CheckRejection()
        {
            var rejectors = Component.GetAllOfTypeFrom<IKnockBackRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<KnockBackRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.KnockBackRejection(Initial, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    CalculationInterrupts,
                    Calculated,
                    Process,
                    RejectionInterrupts,
                    RejectionInterrupts[0].ComponentID
                );
            }

            return null;
        }

        [NotNull]
        private KnockBackResult Succeed()
        {
            return new SucceedResult(Action, CalculationInterrupts, Calculated, Process, RejectionInterrupts);
        }
    }
}
