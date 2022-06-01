using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public class InstantHealEvaluator
    {
        public InstantHealEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            InstantHealAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private InstantHealAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantHealCalculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private Heal Calculated { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantHealRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public InstantHealResult Evaluate()
        {
            InstantHealResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckCondition();
            if (result != null) return result;

            CalculateHeal();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantHealResult FindTarget()
        {
            Target = Context.Finder.FindEntity(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantHealResult CheckCondition()
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

        private void CalculateHeal()
        {
            var effectors = Context.Finder.GetAllComponents<IInstantHealCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<InstantHealCalculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.InstantHealCalculations(Initial, Action, CalculationInterrupts);
                if (interrupts == null) continue;
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }

            Calculated = Action.Heal;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Effect is ReduceCalculation reduceEffect)
                {
                    Calculated = reduceEffect.HealReduction.Caused(Calculated);
                }
            }
        }

        [CanBeNull]
        private InstantHealResult CheckRejection()
        {
            var rejectors = Context.Finder.GetAllComponents<IInstantHealRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<InstantHealRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.InstantHealRejection(Initial, Action, RejectionInterrupts);
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
        private InstantHealResult Succeed()
        {
            return new SucceedResult(Action, CalculationInterrupts, Calculated, RejectionInterrupts);
        }
    }
}
