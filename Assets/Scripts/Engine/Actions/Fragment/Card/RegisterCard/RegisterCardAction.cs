using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public record RegisterCardAction([NotNull] PlayerID ParentID, ID AssignedID, [NotNull] Card InitialProps);
}
