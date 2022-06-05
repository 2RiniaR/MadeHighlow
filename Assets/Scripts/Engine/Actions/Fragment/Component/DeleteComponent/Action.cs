using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public interface IAction
    {
        [NotNull] ComponentID TargetID { get; init; }
    }

    public record Action([NotNull] ComponentID TargetID) : IAction;
}
