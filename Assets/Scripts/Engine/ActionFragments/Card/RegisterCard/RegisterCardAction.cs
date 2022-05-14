using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterCard
{
    public record RegisterCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps)
    {
        public RegisterCardResult Evaluate(IHistory context)
        {
            return new RegisterCardEvaluator(context, ParentID, InitialProps).Evaluate();
        }
    }
}
