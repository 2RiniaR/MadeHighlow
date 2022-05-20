using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public record RejectedResult(
        [NotNull] RunCommandAction Action,
        [NotNull] RunCommandProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandEffect>> Interrupts,
        [CanBeNull] ComponentID RejectedID
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
