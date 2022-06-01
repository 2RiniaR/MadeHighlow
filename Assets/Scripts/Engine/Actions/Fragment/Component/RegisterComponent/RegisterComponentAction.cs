using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record RegisterComponentAction(
        [NotNull] IAttachableID ParentID,
        ID AssignedID,
        [NotNull] Component InitialProps
    );
}
