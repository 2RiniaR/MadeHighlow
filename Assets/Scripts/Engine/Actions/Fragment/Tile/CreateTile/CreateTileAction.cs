using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateTile
{
    public record CreateTileAction([NotNull] Tile InitialProps)
    {
        public CreateTileResult Evaluate(IHistory history)
        {
            return new CreateTileEvaluator(history, this).Evaluate();
        }
    }
}
