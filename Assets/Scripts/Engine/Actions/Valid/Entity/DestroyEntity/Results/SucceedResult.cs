using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
