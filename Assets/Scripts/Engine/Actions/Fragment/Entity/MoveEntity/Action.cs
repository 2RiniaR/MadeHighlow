using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public interface IAction
    {
        [NotNull] EntityID TargetID { get; init; }
        [NotNull] Direction3D Direction { get; init; }
    }

    public record Action([NotNull] EntityID TargetID, [NotNull] Direction3D Direction) : IAction;
}
