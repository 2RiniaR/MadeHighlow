using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public interface IAction
    {
        [NotNull] EntityID TargetID { get; init; }
    }

    public record Action([NotNull] EntityID TargetID) : IAction;
}
