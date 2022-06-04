using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record RegisterComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] RegisterComponent.Result Failed
    ) : Result;
}
