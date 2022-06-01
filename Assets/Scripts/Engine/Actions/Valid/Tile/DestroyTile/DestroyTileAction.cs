using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DestroyTileAction([NotNull] TileID TargetID) : IValidAction;
}
