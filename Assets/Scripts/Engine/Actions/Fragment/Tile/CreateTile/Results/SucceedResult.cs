using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record SucceedResult
        ([NotNull] CreateTileAction Action, [NotNull] CreateTileProcess Process) : CreateTileResult;
}
