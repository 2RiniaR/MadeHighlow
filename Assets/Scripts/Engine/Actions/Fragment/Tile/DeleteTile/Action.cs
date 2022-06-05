using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public interface IAction
    {
        [NotNull] TileID TargetID { get; init; }
    }

    public record Action([NotNull] TileID TargetID) : IAction;
}
