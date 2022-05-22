using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.General.BigBang
{
    public record BigBangAction([NotNull] World InitialWorld) : ValidAction<BigBangResult>
    {
        protected override BigBangResult EvaluateBody(IHistory history)
        {
            return new BigBangEvaluator(history, this).Evaluate();
        }
    }
}
