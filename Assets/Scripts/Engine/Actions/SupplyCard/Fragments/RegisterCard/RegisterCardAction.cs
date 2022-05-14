using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard
{
    public record RegisterCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps)
    {
        public RegisterCardResult Evaluate(IActionContext context)
        {
            return new RegisterCardEvaluator(context, ParentID, InitialProps).Evaluate();
        }
    }
}
