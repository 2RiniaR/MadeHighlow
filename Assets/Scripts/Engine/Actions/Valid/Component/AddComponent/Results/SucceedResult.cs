using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult(
        [NotNull] AddComponentAction Action,
        [NotNull] AddComponentProcess Process
    ) : AddComponentResult;
}
