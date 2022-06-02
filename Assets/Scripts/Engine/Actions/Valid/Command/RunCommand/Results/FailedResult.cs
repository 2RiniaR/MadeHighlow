using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record FailedResult([NotNull] RunCommandAction Action, FailedReason Reason) : RunCommandResult;
}
