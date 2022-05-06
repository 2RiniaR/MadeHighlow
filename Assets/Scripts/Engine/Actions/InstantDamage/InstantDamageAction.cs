using System;
using System.Collections.Generic;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティにダメージを与えるアクション
    /// </summary>
    public record InstantDamageAction : Action<InstantDamageResult>
    {
        /// <summary>
        ///     ダメージを与えるオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     ダメージを受けるエンティティのID
        /// </summary>
        public EntityEnsuredID TargetID { get; init; } = new();

        /// <summary>
        ///     与えるダメージ量
        /// </summary>
        public Damage Damage { get; init; } = new();

        public override InstantDamageResult Validate(in IActionContext context)
        {
            var target = TargetID.GetFrom(context.World) ?? throw new NullReferenceException();

            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantDamageResult { Reason = FailedInstantDamageReason.NoVitality };
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantDamageResult { Reason = FailedInstantDamageReason.AlreadyDead };
            }

            var effectors = Component.GetAllOfTypeFrom<IInstantDamageEffector>(context.World);
            var reductions = new List<DamageReduction>();

            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantDamage(context, this);

                // コンポーネントによって、ダメージが無効化されることがあるよ。無敵エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantDamageResult { DecidedComponentID = effector.EnsuredID };
                }

                // コンポーネントによって、ダメージの量が軽減されることがあるよ。防御エフェクトとかに使えそう。
                if (effect.Reduction != null)
                {
                    reductions.Add(effect.Reduction);
                }
            }

            return new SucceedInstantDamageResult
            {
                SourceID = SourceID,
                TargetID = TargetID,
                Damage = Damage,
                Reductions = reductions.ToValueObjectList(),
            };
        }
    }
}