using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity InitialProps) : ValidAction<GenerateEntityResult>
    {
        protected override GenerateEntityResult EvaluateBody(IHistory history)
        {
            return new GenerateEntityEvaluator(history, this).Evaluate();
        }
    }
}
