using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Tile Positioned) : Result;
}
