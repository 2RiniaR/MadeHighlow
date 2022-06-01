using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public record RegisterTileAction(ID AssignedID, [NotNull] Tile InitialProps);
}
