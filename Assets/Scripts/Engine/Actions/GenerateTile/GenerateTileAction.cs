using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialStatus) : Action<GenerateTileResult>
    {
        protected override GenerateTileResult EvaluateBody(IActionContext context)
        {
            return new GenerateTileEvaluator(context, InitialStatus).Evaluate();
        }
    }
}
