using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public record SucceedResult(
        [NotNull] RunCommandAction Action,
        [NotNull] RunCommandProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandRejection>> RejectionInterrupts
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
