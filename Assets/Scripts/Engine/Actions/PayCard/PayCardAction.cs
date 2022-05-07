namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを対価として支払うアクション
    /// </summary>
    public record PayCardAction(CardID PaidCardID) : Action<PayCardResult>
    {
        public override PayCardResult Validate(IActionContext context)
        {
            foreach (var effector in Component.GetAllOfTypeFrom<IPayCardEffector>(context.World))
            {
                var effect = effector.EffectOnPayCard(context, this);

                // 「この効果が発動してるときは、カードを使っても消費されない」みたいなのができそうだよね
                if (effect.Exempted)
                {
                    return new ExemptedPayCardResult(effector.ComponentID);
                }
            }

            return new SucceedPayCardResult(PaidCardID);
        }
    }
}
