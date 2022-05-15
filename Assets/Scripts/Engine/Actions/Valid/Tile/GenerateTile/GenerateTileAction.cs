using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialStatus) : ValidAction<GenerateTileResult>
    {
        protected override GenerateTileResult EvaluateBody(IHistory history)
        {
            return new GenerateTileEvaluator(history, InitialStatus).Evaluate();
        }
    }
}
