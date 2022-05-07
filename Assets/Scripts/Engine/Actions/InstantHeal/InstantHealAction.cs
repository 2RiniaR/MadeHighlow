using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティに治癒効果を与えるアクション
    /// </summary>
    public record InstantHealAction(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Heal Heal
    ) : Action<InstantHealResult>
    {
        public override InstantHealResult Validate(IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World);

            // 既に対象がいなければ、回復効果は与えられない。
            if (target == null)
            {
                return new FailedInstantHealResult(FailedInstantHealReason.NoTarget);
            }

            // そもそも体力という概念がないものには、治癒効果が与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantHealResult(FailedInstantHealReason.NoVitality);
            }

            // 相手が生きてなければ治癒効果は与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantHealResult(FailedInstantHealReason.AlreadyDead);
            }

            var effectors = Component.GetAllOfTypeFrom<IInstantHealEffector>(context.World);
            var reductions = new List<HealReduction>();

            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantHeal(context, this);

                // コンポーネントによって、治癒効果が無効化されることがあるよ。回復無効エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantHealResult(effector.ComponentID);
                }

                // コンポーネントによって、治癒効果の量が軽減されることがあるよ。回復量減少エフェクトとかに使えそう。
                if (effect.Reduction != null)
                {
                    reductions.Add(effect.Reduction);
                }
            }

            return new SucceedInstantHealResult(SourceID, TargetEntityID, Heal, reductions.ToValueObjectList());
        }
    }
}
