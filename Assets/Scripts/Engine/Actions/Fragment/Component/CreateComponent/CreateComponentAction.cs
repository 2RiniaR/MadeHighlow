using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record CreateComponentAction([NotNull] IAttachableID TargetID, [NotNull] Component InitialStatus);
}
