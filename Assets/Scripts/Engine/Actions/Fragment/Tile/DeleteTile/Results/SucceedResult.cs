using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record SucceedResult
        ([NotNull] DeleteTileAction Action, [NotNull] DeleteTileProcess Process) : DeleteTileResult;
}
