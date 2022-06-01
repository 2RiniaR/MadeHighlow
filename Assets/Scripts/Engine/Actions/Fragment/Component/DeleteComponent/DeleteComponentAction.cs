using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record DeleteComponentAction([NotNull] ComponentID TargetID);
}
