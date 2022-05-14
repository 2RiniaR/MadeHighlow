using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterTile
{
    public record RegisterTileAction([NotNull] Tile InitialProps)
    {
        public RegisterTileResult Evaluate(IHistory history)
        {
            return new RegisterTileEvaluator(history, InitialProps).Evaluate();
        }
    }
}
