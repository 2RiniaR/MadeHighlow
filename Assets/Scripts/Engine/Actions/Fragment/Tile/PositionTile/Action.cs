using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public interface IAction
    {
        [NotNull] TileID TargetID { get; init; }
        [NotNull] Position2D Destination { get; init; }
    }

    public record Action([NotNull] TileID TargetID, [NotNull] Position2D Destination) : IAction;
}
