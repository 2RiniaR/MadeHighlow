using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public class InstantDamageEvaluator
    {
        public InstantDamageEvaluator(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] EntityID targetID,
            [NotNull] Damage expected
        )
        {
            History = history;
            SourceID = sourceID;
            TargetID = targetID;
            Expected = expected;
            Calculated = Expected;
        }

        [NotNull] private IHistory History { get; }
        private ID SourceID { get; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] private Damage Expected { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDamageEffect>> Interrupts { get; set; }
        [CanBeNull] private Damage Calculated { get; set; }

        [NotNull]
        public InstantDamageResult Evaluate()
        {
            InstantDamageResult result;

            result = GetTarget();
            if (result != null) return result;

            result = Validation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantDamageResult GetTarget()
        {
            Contract.Ensures((Contract.Result<InstantDamageResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new FailedResult(FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantDamageResult Validation()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (Target.Vitality == null)
            {
                return new FailedResult(FailedReason.NoVitality);
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (Target.Vitality.IsDead)
            {
                return new FailedResult(FailedReason.TargetDead);
            }

            return null;
        }

        [CanBeNull]
        private InstantDamageResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(History.World);
            Interrupts = effectors
                .SelectMany(effector => effector.EffectsOnInstantDamage(History, SourceID, Target, Expected))
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, Target, Expected, Interrupts, interrupt.ComponentID);
                }

                if (interrupt.Effect is ReduceEffect reduce)
                {
                    Calculated = reduce.DamageReduction.Caused(Calculated);
                }
            }

            return null;
        }

        [NotNull]
        private InstantDamageResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(SourceID, Target, Expected, Interrupts, Calculated);
        }
    }
}
