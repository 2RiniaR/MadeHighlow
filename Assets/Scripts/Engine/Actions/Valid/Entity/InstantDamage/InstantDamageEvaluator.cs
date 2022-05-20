using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public class InstantDamageEvaluator
    {
        public InstantDamageEvaluator([NotNull] IHistory initial, InstantDamageAction action)
        {
            Initial = initial;
            Action = action;
        }

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

            CollectEffectInterrupts();
            CalculateDamage();

            CollectRejectInterrupts();
            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantDamageResult FindTarget()
        {
            Contract.Ensures((Contract.Result<InstantDamageResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantDamageResult CheckCondition()
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

        private void CollectEffectInterrupts()
        {
            Contract.Ensures(CalculationInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<InstantDamageCalculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.InstantDamageCalculations(Initial, Action, CalculationInterrupts);
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }
        }

        private void CalculateDamage()
        {
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Ensures(Calculated != null);

            Calculated = Action.Damage;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Effect is ReduceCalculation reduceEffect)
                {
                    Calculated = reduceEffect.DamageReduction.Caused(Calculated);
                }
            }
        }

        private void CollectRejectInterrupts()
        {
            Contract.Ensures(RejectionInterrupts != null);

            var rejectors = Component.GetAllOfTypeFrom<IInstantDamageRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<InstantDamageRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.InstantDamageRejection(Initial, Action, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }
        }

        [CanBeNull]
        private InstantDamageResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

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
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, CalculationInterrupts, Calculated, RejectionInterrupts);
        }
    }
}
