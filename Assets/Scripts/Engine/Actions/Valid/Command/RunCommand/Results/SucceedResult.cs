using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record SucceedResult(
        [NotNull] RunCommandAction Action,
        [NotNull] RunCommandProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandRejection>> RejectionInterrupts
    ) : RunCommandResult;
}
