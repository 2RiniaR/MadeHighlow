using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
