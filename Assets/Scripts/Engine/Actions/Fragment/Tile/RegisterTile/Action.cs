using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public record Action(ID AssignedID, [NotNull] Tile InitialProps);
}
