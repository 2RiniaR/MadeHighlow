using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialStatus) : Action<GenerateTileResult>
    {
        protected override GenerateTileResult EvaluateBody(IHistory history)
        {
            return new GenerateTileEvaluator(history, InitialStatus).Evaluate();
        }
    }
}
