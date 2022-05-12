using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    /// <summary>
    ///     エンティティに治癒を与えるアクション
    /// </summary>
    public record InstantHealAction
        (ID SourceID, [NotNull] EntityID TargetEntityID, [NotNull] Heal Heal) : Action<InstantHealResult>
    {
        public override InstantHealResult Evaluate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            var calculatedHeal = Heal;
            foreach (var interrupt in interrupts)
            {
                // コンポーネントによって、治癒効果が無効化されることがあるよ。治癒無効エフェクトとかに使えるかも。
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, TargetEntityID, Heal, interrupts, interrupt.ComponentID);
                }

                // コンポーネントによって、治癒効果の量が軽減されることがあるよ。治癒効果減少とかに使えそう。
                if (interrupt.Effect is ReduceEffect reduce)
                {
                    calculatedHeal = reduce.HealReduction.Caused(calculatedHeal);
                }
            }

            return new CausedResult(SourceID, TargetEntityID, Heal, interrupts, calculatedHeal);
        }

        [CanBeNull]
        private InstantHealResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = TargetEntityID.GetFrom(context.World);

            // 既に対象がいなければ、治癒は与えられない。
            if (target == null)
            {
                return new FailedResult(FailedReason.NoTarget);
            }

            // そもそも体力という概念がないものには、治癒が与えられない。
            if (target.Vitality == null)
            {
                return new FailedResult(FailedReason.NoVitality);
            }

            // 相手が生きてなければ治癒は与えられないよ。仕方ないね。
            if (target.Vitality.IsDead)
            {
                return new FailedResult(FailedReason.TargetDead);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<InstantHealEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IInstantHealEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnInstantHeal(context, this));
        }
    }
}
