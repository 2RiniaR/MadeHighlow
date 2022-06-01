using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus
    ) : IValidAction;
}
