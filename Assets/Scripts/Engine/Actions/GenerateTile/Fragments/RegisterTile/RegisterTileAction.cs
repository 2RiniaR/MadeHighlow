using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    public record RegisterTileAction([NotNull] Tile InitialProps)
    {
        public RegisterTileResult Evaluate(IActionContext context)
        {
            return new RegisterTileEvaluator(context, InitialProps).Evaluate();
        }
    }
}
