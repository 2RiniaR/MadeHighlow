using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard.RegisterCard
{
    public interface IAction
    {
        [NotNull] PlayerID ParentID { get; init; }
        ID AssignedID { get; init; }
    }

    public record Action([NotNull] PlayerID ParentID, ID AssignedID, [NotNull] Card InitialProps) : IAction;
}
