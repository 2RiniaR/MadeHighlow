using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PositionTile
{
    public record PositionTileAction([NotNull] TileID TargetID, [NotNull] Position2D Destination)
    {
        public PositionTileResult Evaluate(IActionContext context)
        {
            return new PositionTileEvaluator(context, TargetID, Destination).Evaluate();
        }
    }
}
