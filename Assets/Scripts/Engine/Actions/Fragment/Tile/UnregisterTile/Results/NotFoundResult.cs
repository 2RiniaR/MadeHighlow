using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public record NotFoundResult([NotNull] UnregisterTileAction Action) : UnregisterTileResult;
}
