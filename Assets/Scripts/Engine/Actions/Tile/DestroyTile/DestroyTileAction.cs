using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DestroyTileAction([NotNull] TileID TargetID) : Action<DestroyTileResult>
    {
        protected override DestroyTileResult EvaluateBody(IHistory context)
        {
            return new DestroyTileEvaluator(context, TargetID).Evaluate();
        }
    }
}
