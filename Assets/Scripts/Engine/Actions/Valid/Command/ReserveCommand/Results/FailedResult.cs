using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record FailedResult([NotNull] ReserveCommandAction Action, FailedReason Reason) : ReserveCommandResult;
}
