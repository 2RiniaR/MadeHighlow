using System;
using System.Collections.Generic;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティにダメージを与えるアクション
    /// </summary>
    public record DamageAction : Action<DamageResult>
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

        public override DamageResult Validate(in IActionContext context)
        {
            var target = TargetID.GetFrom(context.World) ?? throw new NullReferenceException();

            // そもそも体力という概念がないものには、ダメージが与えられない。
            if (target.Vitality == null)
            {
                return new FailedDamageResult { Reason = FailedDamageReason.NoVitality };
            }

            // 相手が生きてなければダメージは与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedDamageResult { Reason = FailedDamageReason.AlreadyDead };
            }

            var effectors = Component.GetAllOfTypeFrom<IDamageEffector>(context.World);
            var reductions = new List<DamageReduction>();

            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnDamage(context, this);

                // コンポーネントによって、ダメージが無効化されることがあるよ。無敵エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedDamageResult { DecidedComponentID = effector.EnsuredID };
                }

                // コンポーネントによって、ダメージ量が軽減されることがあるよ。防御エフェクトとかに使えそう。
                if (effect.Reduction != null)
                {
                    reductions.Add(effect.Reduction);
                }
            }

            return new SucceedDamageResult
            {
                SourceID = SourceID,
                TargetID = TargetID,
                Damage = Damage,
                Reductions = reductions.ToValueObjectList(),
            };
        }
    }
}