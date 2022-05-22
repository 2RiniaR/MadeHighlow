using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public record RegisterTileAction(ID AssignedID, [NotNull] Tile InitialProps)
    {
        public RegisterTileResult Evaluate(IHistory history)
        {
            return new RegisterTileEvaluator(history, this).Evaluate();
        }
    }
}
