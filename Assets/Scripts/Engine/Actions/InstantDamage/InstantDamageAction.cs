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

            return new SucceedInstantDamageResult(SourceID, TargetEntityID, Damage, CollectComponentEffects(context));
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
                return new FailedInstantDamageResult(FailedInstantDamageReason.AlreadyDead);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueObjectList<InstantDamageInterrupt> CollectComponentEffects([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(context.World);
            return effectors.Select(
                effector => new InstantDamageInterrupt(
                    effector.ComponentID,
                    effector.EffectOnInstantDamage(context, this)
                )
            );
        }
    }
}
