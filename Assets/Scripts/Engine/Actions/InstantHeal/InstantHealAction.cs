using System;
using System.Collections.Generic;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティに治癒効果を与えるアクション
    /// </summary>
    public record InstantHealAction : Action<InstantHealResult>
    {
        /// <summary>
        ///     治癒効果を与えるオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     治癒効果を受けるエンティティのID
        /// </summary>
        public EntityEnsuredID TargetID { get; init; } = new();

        /// <summary>
        ///     与える治癒効果量
        /// </summary>
        public Heal Heal { get; init; } = new();

        public override InstantHealResult Validate(in IActionContext context)
        {
            var target = TargetID.GetFrom(context.World) ?? throw new NullReferenceException();

            // そもそも体力という概念がないものには、治癒効果が与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantHealResult { Reason = FailedInstantHealReason.NoVitality };
            }

            // 相手が生きてなければ治癒効果は与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantHealResult { Reason = FailedInstantHealReason.AlreadyDead };
            }

            var effectors = Component.GetAllOfTypeFrom<IInstantHealEffector>(context.World);
            var reductions = new List<HealReduction>();

            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantHeal(context, this);

                // コンポーネントによって、治癒効果が無効化されることがあるよ。回復無効エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantHealResult { DecidedComponentID = effector.EnsuredID };
                }

                // コンポーネントによって、治癒効果の量が軽減されることがあるよ。回復量減少エフェクトとかに使えそう。
                if (effect.Reduction != null)
                {
                    reductions.Add(effect.Reduction);
                }
            }

            return new SucceedInstantHealResult
            {
                SourceID = SourceID,
                TargetID = TargetID,
                Heal = Heal,
                Reductions = reductions.ToValueObjectList(),
            };
        }
    }
}