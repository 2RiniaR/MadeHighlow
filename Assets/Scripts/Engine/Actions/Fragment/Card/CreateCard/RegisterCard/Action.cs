using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard.RegisterCard
{
    public record Action([NotNull] PlayerID ParentID, ID AssignedID, [NotNull] Card InitialProps);
}
