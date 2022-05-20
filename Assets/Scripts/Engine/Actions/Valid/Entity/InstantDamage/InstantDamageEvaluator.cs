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
        [CanBeNull] private ValueList<Interrupt<InstantDamageEffect>> EffectInterrupts { get; set; }
        [CanBeNull] private Damage Calculated { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDamageRejection>> RejectInterrupts { get; set; }

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
            Contract.Ensures(EffectInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(Initial.World).Sort();

            EffectInterrupts = ValueList<Interrupt<InstantDamageEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnInstantDamage(Initial, Action, EffectInterrupts);
                EffectInterrupts = EffectInterrupts.AddRange(interrupts);
            }
        }

        private void CalculateDamage()
        {
            Contract.Requires<InvalidOperationException>(EffectInterrupts != null);
            Contract.Ensures(Calculated != null);

            Calculated = Action.Damage;
            foreach (var interrupt in EffectInterrupts)
            {
                if (interrupt.Effect is ReduceEffect reduceEffect)
                {
                    Calculated = reduceEffect.DamageReduction.Caused(Calculated);
                }
            }
        }

        private void CollectRejectInterrupts()
        {
            Contract.Ensures(RejectInterrupts != null);

            var rejectors = Component.GetAllOfTypeFrom<IInstantDamageRejector>(Initial.World).Sort();

            RejectInterrupts = ValueList<Interrupt<InstantDamageRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.OnInstantDamage(Initial, Action, RejectInterrupts);
                RejectInterrupts = RejectInterrupts.Add(interrupt);
            }
        }

        [CanBeNull]
        private InstantDamageResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(EffectInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectInterrupts != null);

            if (!RejectInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    EffectInterrupts,
                    Calculated,
                    RejectInterrupts,
                    RejectInterrupts[0].ComponentID
                );
            }

            return null;
        }

        [NotNull]
        private InstantDamageResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(EffectInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectInterrupts != null);

            return new SucceedResult(Action, EffectInterrupts, Calculated, RejectInterrupts);
        }
    }
}
