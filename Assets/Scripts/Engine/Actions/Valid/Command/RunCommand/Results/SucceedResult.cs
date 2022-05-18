using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public record SucceedResult(
        [NotNull] RunCommandAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandEffect>> Interrupts
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
