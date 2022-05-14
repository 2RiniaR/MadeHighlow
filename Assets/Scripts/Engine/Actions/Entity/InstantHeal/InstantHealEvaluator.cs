using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public class InstantHealEvaluator
    {
        public InstantHealEvaluator(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] EntityID targetID,
            [NotNull] Heal expected
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
        [NotNull] private Heal Expected { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantHealEffect>> Interrupts { get; set; }
        [CanBeNull] private Heal Calculated { get; set; }

        [NotNull]
        public InstantHealResult Evaluate()
        {
            InstantHealResult result;

            result = GetTarget();
            if (result != null) return result;

            result = Validation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        private InstantHealResult GetTarget()
        {
            Contract.Ensures((Contract.Result<InstantHealResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new FailedResult(FailedReason.NoTarget);
            }

            return null;
        }

        private InstantHealResult Validation()
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

        private InstantHealResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantHealEffector>(History.World);
            Interrupts = effectors
                .SelectMany(effector => effector.EffectsOnInstantHeal(History, SourceID, Target, Expected))
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, Target, Expected, Interrupts, interrupt.ComponentID);
                }

                if (interrupt.Effect is ReduceEffect reduce)
                {
                    Calculated = reduce.HealReduction.Caused(Calculated);
                }
            }

            return null;
        }

        private InstantHealResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(SourceID, Target, Expected, Interrupts, Calculated);
        }
    }
}
