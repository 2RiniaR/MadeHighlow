using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public class ActionEvaluator
    {
        public ActionEvaluator(
            [NotNull] IActionContext context,
            ID sourceID,
            [NotNull] EntityID targetID,
            [NotNull] Damage expected
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
        [NotNull] private Damage Expected { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDamageEffect>> Interrupts { get; set; }
        [CanBeNull] private Damage Calculated { get; set; }

        [NotNull]
        public InstantDamageResult Evaluate()
        {
            Contract.Ensures(Contract.Result<InstantDamageResult>() != null);

            InstantDamageResult error;

            error = GetTarget();
            if (error != null) return error;

            error = Validation();
            if (error != null) return error;

            error = CollectInterrupts();
            if (error != null) return error;

            return Succeed();
        }

        [CanBeNull]
        private InstantDamageResult GetTarget()
        {
            Target = TargetID.GetFrom(Context.World);
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

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(Context.World);
            Interrupts = effectors
                .SelectMany(effector => effector.EffectsOnInstantDamage(Context, SourceID, Target, Expected))
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
