namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを対価として支払うアクション
    /// </summary>
    public record PayCardAction : Action<PayCardResult>
    {
        /// <summary>
        ///     対価として支払うカードのID
        /// </summary>
        public CardEnsuredID PaidCardID { get; init; } = new();

        public override PayCardResult Validate(in IActionContext context)
        {
            var world = context.CurrentWorld();

            foreach (var effector in Component.GetAllOfTypeFrom<IPayCardEffector>(world))
            {
                var effect = effector.EffectOnPayCard(context, this);

                // 「この効果が発動してるときは、カードを使っても消費されない」みたいなのができそうだよね
                if (effect.Exempted)
                {
                    return new ExemptedPayCardResult { DecidedComponentID = effector.EnsuredID };
                }
            }

            return new SucceedPayCardResult { PaidCardID = PaidCardID };
        }
    }
}