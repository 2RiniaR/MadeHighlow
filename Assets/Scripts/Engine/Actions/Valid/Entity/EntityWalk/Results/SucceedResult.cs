using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
