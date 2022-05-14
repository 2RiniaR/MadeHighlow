using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record ElevateTileAction
        (ID SourceID, [NotNull] TileID TargetID, [NotNull] Elevate Elevate) : Action<ElevateTileResult>
    {
        protected override ElevateTileResult EvaluateBody(IActionContext context)
        {
            return new ElevateTileEvaluator(context, SourceID, TargetID, Elevate).Evaluate();
        }
    }
}
