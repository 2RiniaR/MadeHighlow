using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record DeleteComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] DeleteComponent.Result Failed
    ) : Result;
}
