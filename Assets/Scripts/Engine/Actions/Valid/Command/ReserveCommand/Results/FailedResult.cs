using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
