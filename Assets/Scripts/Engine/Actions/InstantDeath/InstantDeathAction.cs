using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えるアクション
    /// </summary>
    public record InstantDeathAction : Action<InstantDeathResult>
    {
        /// <summary>
        ///     即死効果を与えるオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     即死効果を受けるエンティティのID
        /// </summary>
        public EntityEnsuredID TargetID { get; init; } = new();

        public override InstantDeathResult Validate(in IActionContext context)
        {
            var target = TargetID.GetFrom(context.World) ?? throw new NullReferenceException();

            // そもそも生命という概念がないものには、即死効果は与えられない。
            if (target.Vitality == null)
            {
                return new FailedInstantDeathResult { Reason = FailedInstantDeathReason.NoVitality };
            }

            // 相手が生きてなければ即死効果は与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedInstantDeathResult { Reason = FailedInstantDeathReason.AlreadyDead };
            }

            var effectors = Component.GetAllOfTypeFrom<IInstantDeathEffector>(context.World);
            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnInstantDeath(context, this);

                // コンポーネントによって、即死効果が無効化されることがあるよ。即死無効エフェクトとかに使えるかも。
                if (effect.Refused)
                {
                    return new RefusedInstantDeathResult { DecidedComponentID = effector.EnsuredID };
                }
            }

            return new SucceedInstantDeathResult
            {
                SourceID = SourceID,
                TargetID = TargetID,
            };
        }
    }
}