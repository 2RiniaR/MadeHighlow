using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record CanceledResult(
        [NotNull] Command Command,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandEffect>> Interrupts,
        [CanBeNull] ComponentID DisallowedID
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
