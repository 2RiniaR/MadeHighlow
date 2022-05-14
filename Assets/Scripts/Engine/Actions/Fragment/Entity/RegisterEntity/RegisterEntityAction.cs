using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterEntity
{
    public record RegisterEntityAction([NotNull] Entity InitialProps)
    {
        public RegisterEntityResult Evaluate(IHistory history)
        {
            return new RegisterEntityEvaluator(history, InitialProps).Evaluate();
        }
    }
}
