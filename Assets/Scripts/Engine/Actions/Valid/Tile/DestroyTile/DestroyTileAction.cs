using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DestroyTileAction([NotNull] TileID TargetID) : ValidAction<DestroyTileResult>
    {
        protected override DestroyTileResult EvaluateBody(IHistory history)
        {
            return new DestroyTileEvaluator(history, this).Evaluate();
        }
    }
}
