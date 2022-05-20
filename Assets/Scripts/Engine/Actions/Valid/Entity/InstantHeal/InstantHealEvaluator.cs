using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public class InstantHealEvaluator
    {
        public InstantHealEvaluator([NotNull] IHistory initial, InstantHealAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            Contract.Ensures((Contract.Result<InstantHealResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantHealResult CheckCondition()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

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
            Contract.Ensures(CalculationInterrupts != null);
            Contract.Ensures(Calculated != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantHealCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<InstantHealCalculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.InstantHealCalculations(Initial, Action, CalculationInterrupts);
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
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Ensures(RejectionInterrupts != null);

            var rejectors = Component.GetAllOfTypeFrom<IInstantHealRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<InstantHealRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.InstantHealRejection(Initial, Action, RejectionInterrupts);
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
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, CalculationInterrupts, Calculated, RejectionInterrupts);
        }
    }
}
