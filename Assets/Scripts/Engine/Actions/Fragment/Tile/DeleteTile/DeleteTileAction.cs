using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record DeleteTileAction([NotNull] TileID TargetID)
    {
        public DeleteTileResult Evaluate(IHistory history)
        {
            return new DeleteTileEvaluator(history, this).Evaluate();
        }
    }
}
