using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティにダメージを与えるアクション
    /// </summary>
    public record InstantDamageAction
        (ID SourceID, [NotNull] EntityID TargetEntityID, [NotNull] Damage Damage) : Action<InstantDamageResult>
    {
        public override InstantDamageResult Validate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            var calculatedDamage = Damage;
            foreach (var interrupt in interrupts)
            {
                // コンポーネントによって、治癒効果が無効化されることがあるよ。無敵エフェクトとかに使えるかも。
                if (interrupt.Effect is DamageRejectionEffect)
                {
                    return new RejectedInstantDamageResult(
                        SourceID,
                        TargetEntityID,
                        Damage,
                        interrupts,
                        interrupt.ComponentID
                    );
                }

                // コンポーネントによって、治癒効果の量が軽減されることがあるよ。防御エフェクトとかに使えそう。
                if (interrupt.Effect is DamageReductionEffect effect)
                {
                    calculatedDamage = effect.DamageReduction.Caused(calculatedDamage);
                }
            }

            return new CausedInstantDamageResult(SourceID, TargetEntityID, Damage, interrupts, calculatedDamage);
        }

        [CanBeNull]
        private InstantDamageResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World);

            // 既に対象がいなければ、ダメージは与えられない。
            if (target == null)
            {
                return new FailedInstantDamageResult(FailedInstantDamageReason.NoTarget);
            }

            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantDamageResult(FailedInstantDamageReason.NoVitality);
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantDamageResult(FailedInstantDamageReason.TargetDead);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<InstantDamageEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnInstantDamage(context, this));
        }
    }
}
