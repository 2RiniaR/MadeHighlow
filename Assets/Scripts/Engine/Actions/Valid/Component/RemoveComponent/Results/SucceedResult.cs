using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
