using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record CreateComponentFailedResult(
        [NotNull] AddComponentAction Action,
        [NotNull] CreateComponentResult Failed
    ) : AddComponentResult;
}
