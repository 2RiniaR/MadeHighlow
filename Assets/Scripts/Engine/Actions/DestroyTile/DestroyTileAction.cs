using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DestroyTileAction([NotNull] TileID TargetID) : Action<DestroyTileResult>
    {
        public override DestroyTileResult Evaluate(IActionContext context)
        {
            return new DestroyTileEvaluator(context, TargetID).Evaluate();
        }
    }
}
