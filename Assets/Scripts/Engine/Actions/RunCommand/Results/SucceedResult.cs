using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record SucceedResult(
        [NotNull] Command Command,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandEffect>> Interrupts,
        [NotNull] ValueList<Result> CommandActionResults,
        [NotNull] PayCardResult PayCardResult
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(CommandActionResults).Then(PayCardResult).Simulate(world);
        }
    }
}
