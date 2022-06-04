using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record CreateComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] CreateComponent.Result Failed
    ) : Result;
}
