using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public class InstantHealEvaluator
    {
        public InstantHealEvaluator(
            [NotNull] IActionContext context,
            ID sourceID,
            [NotNull] EntityID targetID,
            [NotNull] Heal expected
        )
        {
            Context = context;
            SourceID = sourceID;
            TargetID = targetID;
            Expected = expected;
            Calculated = Expected;
        }

        [NotNull] private IActionContext Context { get; }
        private ID SourceID { get; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] private Heal Expected { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantHealEffect>> Interrupts { get; set; }
        [CanBeNull] private Heal Calculated { get; set; }

        [NotNull]
        public InstantHealResult Evaluate()
        {
            Contract.Ensures(Contract.Result<InstantHealResult>() != null);

            InstantHealResult error;

            error = GetTarget();
            if (error != null) return error;

            error = Validation();
            if (error != null) return error;

            error = CollectInterrupts();
            if (error != null) return error;

            return Succeed();
        }

        private InstantHealResult GetTarget()
        {
            Target = TargetID.GetFrom(Context.World);
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

            var effectors = Component.GetAllOfTypeFrom<IInstantHealEffector>(Context.World);
            Interrupts = effectors
                .SelectMany(effector => effector.EffectsOnInstantHeal(Context, SourceID, Target, Expected))
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
