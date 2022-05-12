using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateTile.PositionTile;
using RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record FailedProcess(
        [CanBeNull] RegisterTileResult RegisterTile,
        [CanBeNull] [ItemNotNull] ValueList<AddComponentResult> AddComponents,
        [CanBeNull] PositionTileResult PositionTile
    );
}
