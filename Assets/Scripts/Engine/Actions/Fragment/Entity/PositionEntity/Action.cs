using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public interface IAction
    {
        [NotNull] EntityID TargetID { get; init; }
        [NotNull] Position3D Destination { get; init; }
    }

    public record Action([NotNull] EntityID TargetID, [NotNull] Position3D Destination) : IAction;
}
