using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record ParentNotFoundResult([NotNull] RegisterComponentAction Action) : RegisterComponentResult;
}
