using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterTile
{
    public record UnregisterTileAction([NotNull] TileID TargetID)
    {
        public UnregisterTileResult Evaluate(IHistory history)
        {
            return new UnregisterTileEvaluator(history, this).Evaluate();
        }
    }
}
