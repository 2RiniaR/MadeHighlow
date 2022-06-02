using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record ElevateTileAction(ID SourceID, [NotNull] TileID TargetID, [NotNull] Elevate Elevate) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.ElevateTile(history, this);
        }
    }
}
