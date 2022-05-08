using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティに即死効果を与えるアクション
    /// </summary>
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetEntityID) : Action<InstantDeathResult>
    {
        public override InstantDeathResult Validate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                // コンポーネントによって、治癒効果が無効化されることがあるよ。無敵エフェクトとかに使えるかも。
                if (interrupt.Effect is DeathRejectionEffect)
                {
                    return new RejectedInstantDeathResult(SourceID, TargetEntityID, interrupts, interrupt.ComponentID);
                }
            }

            return new CausedInstantDeathResult(SourceID, TargetEntityID, interrupts);
        }

        [CanBeNull]
        private InstantDeathResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World);

            // 既に対象がいなければ、ダメージは与えられない。
            if (target == null)
            {
                return new FailedInstantDeathResult(FailedInstantDeathReason.NoTarget);
            }

            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantDeathResult(FailedInstantDeathReason.NoVitality);
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantDeathResult(FailedInstantDeathReason.TargetDead);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<InstantDeathEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IInstantDeathEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnInstantDeath(context, this));
        }
    }
}
