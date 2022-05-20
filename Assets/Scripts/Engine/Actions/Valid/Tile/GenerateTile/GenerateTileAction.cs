using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialProps) : ValidAction<GenerateTileResult>
    {
        protected override GenerateTileResult EvaluateBody(IHistory history)
        {
            return new GenerateTileEvaluator(history, this).Evaluate();
        }
    }
}
