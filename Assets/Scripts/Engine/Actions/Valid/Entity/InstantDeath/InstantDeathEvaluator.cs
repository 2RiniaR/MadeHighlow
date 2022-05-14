using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public class InstantDeathEvaluator
    {
        public InstantDeathEvaluator([NotNull] IHistory history, ID sourceID, [NotNull] EntityID targetID)
        {
            History = history;
            SourceID = sourceID;
            TargetID = targetID;
        }

        [NotNull] private IHistory History { get; }
        private ID SourceID { get; }
        [NotNull] private EntityID TargetID { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDeathEffect>> Interrupts { get; set; }

        [NotNull]
        public InstantDeathResult Evaluate()
        {
            InstantDeathResult result;

            result = GetTarget();
            if (result != null) return result;

            result = Validation();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantDeathResult GetTarget()
        {
            Contract.Ensures((Contract.Result<InstantDeathResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new FailedResult(FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantDeathResult Validation()
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
        private InstantDeathResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IInstantDeathEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnInstantDeath(History, SourceID, Target))
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, Target, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private InstantDeathResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(SourceID, Target, Interrupts);
        }
    }
}
