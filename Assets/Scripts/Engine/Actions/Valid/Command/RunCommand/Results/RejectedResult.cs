using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RejectedResult(
        [NotNull] RunCommandAction Action,
        [NotNull] RunCommandProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandRejection>> RejectionInterrupts,
        [CanBeNull] ComponentID RejectedID
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
