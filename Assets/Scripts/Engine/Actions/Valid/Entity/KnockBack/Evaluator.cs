using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityFly;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<Calculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private KnockBack Calculated { get; set; }

        [CanBeNull] private Event<ReactedResult<EntityFly.SucceedResult>> EntityFlyEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindTarget();
            if (result != null) return result;

            CalculateKnockBack();

            result = Fly();
            if (result != null) return result;

            WrapProcess();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(
                    history,
                    collected,
                    Action,
                    CalculationInterrupts,
                    Calculated,
                    Process
                ),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    CalculationInterrupts,
                    Calculated,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindTarget()
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
            var effectors = Context.Finder.GetAllComponents<ICalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<Calculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.KnockBackCalculations(Initial, Action, CalculationInterrupts);
                if (interrupts == null) continue;
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }

            Calculated = Action.KnockBack;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Content is Reduction reduction)
                {
                    Calculated = reduction.Value.Caused(Calculated);
                }
            }
        }

        [CanBeNull]
        private Result Fly()
        {
            var steps = Enumerable.Range(0, Calculated.Distance.Value)
                .Select(_ => new Step(Calculated.Direction))
                .ToValueList();
            var result = Context.Actions.EntityFly(Simulating, new EntityFly.Action(Action.TargetID, new Route(steps)));
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
            Process = new Process(EntityFlyEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, CalculationInterrupts, Calculated, Process);
        }
    }
}
