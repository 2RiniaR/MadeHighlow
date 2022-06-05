using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public interface IAction
    {
        [NotNull] CardID TargetID { get; init; }
    }

    public record Action([NotNull] CardID TargetID) : IAction;
}
