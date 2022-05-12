using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record SucceedProcess(
        [NotNull] RegisterTile.SucceedResult RegisterTile,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponents,
        [NotNull] PositionTile.SucceedResult PositionTile
    );
}
