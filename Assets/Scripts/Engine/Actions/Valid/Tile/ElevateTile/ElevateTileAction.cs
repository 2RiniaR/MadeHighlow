using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record ElevateTileAction
        (ID SourceID, [NotNull] TileID TargetID, [NotNull] Elevate Elevate) : ValidAction<ElevateTileResult>
    {
        protected override ElevateTileResult EvaluateBody(IHistory history)
        {
            return new ElevateTileEvaluator(history, this).Evaluate();
        }
    }
}
