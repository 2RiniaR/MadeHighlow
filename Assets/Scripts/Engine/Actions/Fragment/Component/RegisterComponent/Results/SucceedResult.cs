using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record SucceedResult
        ([NotNull] RegisterComponentAction Action, [NotNull] Component Registered) : RegisterComponentResult;
}
