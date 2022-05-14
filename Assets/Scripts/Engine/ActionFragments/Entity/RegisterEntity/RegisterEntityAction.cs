using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterEntity
{
    public record RegisterEntityAction([NotNull] Entity InitialProps)
    {
        public RegisterEntityResult Evaluate(IHistory context)
        {
            return new RegisterEntityEvaluator(context, InitialProps).Evaluate();
        }
    }
}
