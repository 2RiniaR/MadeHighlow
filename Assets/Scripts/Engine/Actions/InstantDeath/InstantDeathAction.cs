using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えるアクション
    /// </summary>
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetEntityID) : Action<InstantDeathResult>
    {
        public override InstantDeathResult Validate(IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World) ?? throw new NullReferenceException();

            // そもそも生命という概念がないものには、即死効果は与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantDeathResult(FailedInstantDeathReason.NoVitality);
            }

            // 相手が生きてなければ即死効果は与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantDeathResult(FailedInstantDeathReason.AlreadyDead);
            }

            var effectors = Component.GetAllOfTypeFrom<IInstantDeathEffector>(context.World);
            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantDeath(context, this);

                // コンポーネントによって、即死効果が無効化されることがあるよ。即死無効エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantDeathResult(effector.ComponentID);
                }
            }

            return new SucceedInstantDeathResult(SourceID, TargetEntityID);
        }
    }
}