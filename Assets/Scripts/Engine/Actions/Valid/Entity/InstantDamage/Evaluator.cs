using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, [NotNull] Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<Calculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private Damage Calculated { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckCondition();
            if (result != null) return result;

            CalculateDamage();

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(
                    history,
                    collected,
                    Action,
                    CalculationInterrupts,
                    Calculated
                ),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    CalculationInterrupts,
                    Calculated,
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
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private Result CheckCondition()
        {
            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (Target.Vitality == null)
            {
                return new FailedResult(Action, FailedReason.NoVitality);
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (Target.Vitality.IsDead)
            {
                return new FailedResult(Action, FailedReason.TargetDead);
            }

            return null;
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
                if (interrupt.Effect is ReduceCalculation reduceEffect)
                {
                    Calculated = reduceEffect.DamageReduction.Caused(Calculated);
                }
            }
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, CalculationInterrupts, Calculated);
        }
    }
}
