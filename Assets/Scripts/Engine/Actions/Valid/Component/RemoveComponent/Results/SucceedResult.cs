using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record SucceedResult(
        [NotNull] RemoveComponentAction Action,
        [NotNull] RemoveComponentProcess Process
    ) : RemoveComponentResult;
}
