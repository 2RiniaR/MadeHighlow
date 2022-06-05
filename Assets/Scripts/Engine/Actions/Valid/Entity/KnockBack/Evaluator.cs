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
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var target = FindTarget();

            if (target == null) return Result;

            CalculateKnockBack();
            Fly();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result with { IsConfirmed = true };
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private void CalculateKnockBack()
        {
            var effectors = Context.Finder.GetAllComponents<ICalculator>(Initial.World).Sort();

            var calculations = ValueList<Interrupt<Calculation>>.Empty;
            foreach (var effector in effectors)
            {
                var context = new CalculationContext(Simulating, Result, calculations);
                var interrupts = effector.Calculations(context);
                if (interrupts == null) continue;
                calculations = calculations.AddRange(interrupts);
            }

            var knockBack = Action.KnockBack;
            foreach (var calculation in calculations)
            {
                if (calculation.Content.Reduction != null)
                {
                    knockBack = calculation.Content.Reduction.Caused(knockBack);
                }
            }

            Result = Result with
            {
                Calculations = calculations,
                Calculated = knockBack,
            };
        }

        private void Fly()
        {
            var steps = Enumerable.Range(0, Result.Calculated.Distance.Value)
                .Select(_ => new Step(Result.Calculated.Direction))
                .ToValueList();

            var action = new EntityFly.Action(Action.TargetID, new Route(steps));
            var result = Context.Actions.EntityFly(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { EntityFly = @event };
        }
    }
}
