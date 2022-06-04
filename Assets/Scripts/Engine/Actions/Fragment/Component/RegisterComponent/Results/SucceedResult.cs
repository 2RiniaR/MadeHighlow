using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Component Registered) : Result;
}
