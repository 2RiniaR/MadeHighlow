using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public record SucceedResult([NotNull] UnregisterTileAction Action) : UnregisterTileResult;
}
