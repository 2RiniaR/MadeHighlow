using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record ElevateTileAction
        (ID SourceID, [NotNull] TileID TargetID, [NotNull] Elevate Elevate) : Action<ElevateTileResult>
    {
        public override ElevateTileResult Evaluate(IActionContext context)
        {
            return new ActionEvaluator(context, SourceID, TargetID, Elevate).Evaluate();
        }
    }
}
