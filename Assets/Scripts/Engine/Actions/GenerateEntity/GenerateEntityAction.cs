using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity InitialStatus) : Action<GenerateEntityResult>
    {
        public override GenerateEntityResult Evaluate(IActionContext context)
        {
            return new GenerateEntityEvaluator(context, InitialStatus).Evaluate();
        }
    }
}
