using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードを対価として支払うアクション
    /// </summary>
    public record PayCardAction([NotNull] CardID TargetID) : Action<PayCardResult>
    {
        public override PayCardResult Evaluate(IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);
            if (target == null)
            {
                return new NotFoundResult(TargetID);
            }

            var effectors = Component.GetAllOfTypeFrom<IPayCardEffector>(context.World);
            var interrupts = effectors.SelectMany(effector => effector.EffectsOnPayCard(context, target)).Sort();
            foreach (var interrupt in interrupts)
            {
                // 「この効果が発動してるときは、カードを使っても消費されない」みたいなのができそうだよね
                if (interrupt.Effect is ExemptEffect)
                {
                    return new ExemptedResult(target, interrupt.ComponentID);
                }
            }

            return new SucceedResult(target);
        }
    }
}
