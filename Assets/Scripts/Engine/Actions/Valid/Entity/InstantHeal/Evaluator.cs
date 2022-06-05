using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public class Evaluator
    {
        [NotNull]
        public delegate CalculationContext ContextProvider(
            [NotNull] IHistory history,
            [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> collected
        );

        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var target = FindTarget();

            if (target == null) return Result;
            if (!IsValid(target)) return Result;

            CalculateHeal();
            var healed = Healed(target);

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(
                    history,
                    Result with { Healed = healed },
                    collected
                ),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result with { Healed = healed };
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private bool IsValid([NotNull] Entity target)
        {
            if (target.Vitality == null) return false;
            if (target.Vitality.IsDead) return false;

            return true;
        }

        private void CalculateHeal()
        {
            var effectors = Context.Finder.GetAllComponents<ICalculator>(Initial.World).Sort();

            var calculations = ValueList<Interrupt<Calculation>>.Empty;
            foreach (var effector in effectors)
            {
                var context = new CalculationContext(Initial, Result, calculations);
                var interrupts = effector.Calculations(context);
                if (interrupts == null) continue;
                calculations = calculations.AddRange(interrupts);
            }

            var heal = Action.Heal;
            foreach (var calculation in calculations)
            {
                if (calculation.Content.Reduction != null)
                {
                    heal = calculation.Content.Reduction.Caused(heal);
                }
            }

            Result = Result with
            {
                Calculations = calculations,
                Calculated = heal,
            };
        }

        [NotNull]
        private Entity Healed([NotNull] Entity target)
        {
            return target with { Vitality = Result.Calculated.Caused(target.Vitality) };
        }
    }
}
