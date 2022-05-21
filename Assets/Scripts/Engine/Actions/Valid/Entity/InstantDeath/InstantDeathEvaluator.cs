using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public class InstantDeathEvaluator
    {
        public InstantDeathEvaluator([NotNull] IHistory initial, InstantDeathAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private InstantDeathAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<InstantDeathRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public InstantDeathResult Evaluate()
        {
            InstantDeathResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckCondition();
            if (result != null) return result;

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private InstantDeathResult FindTarget()
        {
            Contract.Ensures((Contract.Result<InstantDeathResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.NoTarget);
            }

            return null;
        }

        [CanBeNull]
        private InstantDeathResult CheckCondition()
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

        [CanBeNull]
        private InstantDeathResult CheckRejection()
        {
            Contract.Ensures(RejectionInterrupts != null);

            var rejectors = Component.GetAllOfTypeFrom<IInstantDeathRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<InstantDeathRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.InstantDeathRejection(Initial, Action, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private InstantDeathResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
