using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
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
        [NotNull] private IHistory Simulating { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var target = FindTarget();

            if (target == null) return Result;
            if (!IsValid(target)) return Result;

            CalculateDamage();
            DamageEntity(target);

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result with { Confirmed = true };
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

        private void CalculateDamage()
        {
            var effectors = Context.Finder.GetAllComponents<ICalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<Calculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.InstantDamageCalculations(Initial, Action, CalculationInterrupts);
                if (interrupts == null) continue;
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }

            Calculated = Action.Damage;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Content is ReduceCalculation reduceEffect)
                {
                    Calculated = reduceEffect.DamageReduction.Caused(Calculated);
                }
            }
        }

        private void DamageEntity([NotNull] Entity target)
        {
            var damagedTarget = target with { Vitality = Result.Calculated.Caused(target.Vitality) };
            Result = Result with { Damaged = damagedTarget };
        }
    }
}
