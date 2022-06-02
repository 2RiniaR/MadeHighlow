using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialProps) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.GenerateTile(history, this);
        }
    }
}
