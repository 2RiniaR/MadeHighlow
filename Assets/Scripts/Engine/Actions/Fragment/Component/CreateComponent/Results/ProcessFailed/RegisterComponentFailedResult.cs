using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record RegisterComponentFailedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] RegisterComponentResult Failed
    ) : CreateComponentResult;
}
