using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record Action([NotNull] IAttachableID ParentID, ID AssignedID, [NotNull] Component InitialProps);
}
