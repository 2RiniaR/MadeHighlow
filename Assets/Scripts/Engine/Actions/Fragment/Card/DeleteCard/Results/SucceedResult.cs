using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
