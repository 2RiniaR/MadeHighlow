using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity InitialStatus) : Action<GenerateEntityResult>
    {
        protected override GenerateEntityResult EvaluateBody(IHistory history)
        {
            return new GenerateEntityEvaluator(history, InitialStatus).Evaluate();
        }
    }
}
