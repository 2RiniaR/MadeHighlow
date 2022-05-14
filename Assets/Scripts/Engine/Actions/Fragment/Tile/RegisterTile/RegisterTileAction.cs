using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterTile
{
    public record RegisterTileAction([NotNull] Tile InitialProps)
    {
        public RegisterTileResult Evaluate(IHistory history)
        {
            return new RegisterTileEvaluator(history, InitialProps).Evaluate();
        }
    }
}
