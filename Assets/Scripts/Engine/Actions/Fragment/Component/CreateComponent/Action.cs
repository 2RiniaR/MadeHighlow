using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record Action([NotNull] IAttachableID TargetID, [NotNull] Component InitialStatus);
}
