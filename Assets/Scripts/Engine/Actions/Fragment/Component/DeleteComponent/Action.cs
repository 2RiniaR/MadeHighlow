using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record Action([NotNull] ComponentID TargetID);
}
