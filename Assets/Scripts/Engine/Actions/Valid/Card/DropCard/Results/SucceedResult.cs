using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
