using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile.RegisterTile
{
    public record Action(ID AssignedID, [NotNull] Tile InitialProps);
}
