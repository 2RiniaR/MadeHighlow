using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record FailedResult([NotNull] PositionTileAction Action, FailedReason Reason) : PositionTileResult;
}
