using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードを対価として支払うアクション
    /// </summary>
    public record PayCardAction([NotNull] CardID CardID) : Action<PayCardResult>
    {
        public override PayCardResult Validate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                // 「この効果が発動してるときは、カードを使っても消費されない」みたいなのができそうだよね
                if (interrupt.Effect is ExemptEffect)
                {
                    return new ExemptedResult(CardID, interrupt.ComponentID);
                }
            }

            return new SucceedResult(CardID);
        }

        [CanBeNull]
        private PayCardResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = CardID.GetFrom(context.World);

            // 既に支払うカードがなければ、カードは支払えない。
            if (target == null)
            {
                return new FailedResult(CardID, FailedReason.NoCard);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<PayCardEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IPayCardEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnPayCard(context, this));
        }
    }
}
