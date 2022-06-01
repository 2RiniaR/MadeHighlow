using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public class InstantDamageEvaluator
    {
        public InstantDamageEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] InstantDamageAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private InstantDamageAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDamageCalculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private Damage Calculated { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDamageRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public InstantDamageResult Evaluate()
        {
            InstantDamageResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckCondition();
            if (result != null) return result;

            CalculateDamage();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantDamageResult FindTarget()
        {
            Target = Context.Finder.FindEntity(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantDamageResult CheckCondition()
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
            var effectors = Context.Finder.GetAllComponents<IInstantDamageCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<InstantDamageCalculation>>.Empty;
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

        [CanBeNull]
        private InstantDamageResult CheckRejection()
        {
            var rejectors = Context.Finder.GetAllComponents<IInstantDamageRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<InstantDamageRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.InstantDamageRejection(Initial, Action, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    CalculationInterrupts,
                    Calculated,
                    RejectionInterrupts,
                    RejectionInterrupts[0].ComponentID
                );
            }

            return null;
        }

        [NotNull]
        private InstantDamageResult Succeed()
        {
            return new SucceedResult(Action, CalculationInterrupts, Calculated, RejectionInterrupts);
        }
    }
}
