using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
