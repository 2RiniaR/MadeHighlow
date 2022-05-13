using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.PositionTile
{
    public record PositionTileAction
        ([NotNull] TileID TargetID, [NotNull] Position2D Destination) : Action<PositionTileResult>
    {
        public override PositionTileResult Evaluate(IActionContext context)
        {
            return new PositionTileEvaluator(context, TargetID, Destination).Evaluate();
        }
    }
}
