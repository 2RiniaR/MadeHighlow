using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public interface IAction
    {
        [NotNull] IAttachableID TargetID { get; init; }
        [NotNull] Component InitialStatus { get; init; }
    }

    public record Action([NotNull] IAttachableID TargetID, [NotNull] Component InitialStatus) : IAction;
}
