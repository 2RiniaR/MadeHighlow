using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionTile
{
    public record PositionTileAction([NotNull] TileID TargetID, [NotNull] Position2D Destination)
    {
        public PositionTileResult Evaluate(IHistory history)
        {
            return new PositionTileEvaluator(history, this).Evaluate();
        }
    }
}
