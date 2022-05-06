using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティにダメージを与えるアクション
    /// </summary>
    public record InstantDamageAction(
        in ID SourceID,
        [NotNull] in EntityID TargetEntityID,
        [NotNull] in Damage Damage
    ) : Action<InstantDamageResult>
    {
        public override InstantDamageResult Validate(in IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World) ?? throw new NullReferenceException();

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

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(context.World);
            var reductions = new List<DamageReduction>();

            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantDamage(context, this);

                // コンポーネントによって、ダメージが無効化されることがあるよ。無敵エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantDamageResult(effector.ComponentID);
                }

                // コンポーネントによって、ダメージの量が軽減されることがあるよ。防御エフェクトとかに使えそう。
                if (effect.Reduction != null)
                {
                    reductions.Add(effect.Reduction);
                }
            }

            return new SucceedInstantDamageResult(SourceID, TargetEntityID, Damage, reductions.ToValueObjectList());
        }
    }
}