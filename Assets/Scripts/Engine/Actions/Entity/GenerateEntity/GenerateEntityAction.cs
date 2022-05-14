using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity InitialStatus) : Action<GenerateEntityResult>
    {
        protected override GenerateEntityResult EvaluateBody(IHistory context)
        {
            return new GenerateEntityEvaluator(context, InitialStatus).Evaluate();
        }
    }
}
