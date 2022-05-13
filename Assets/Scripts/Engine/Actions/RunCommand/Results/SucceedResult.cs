using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record SucceedResult(
        [NotNull] Command Command,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandEffect>> Interrupts,
        [NotNull] PayCardResult PayCardResult,
        [NotNull] ValueList<Result> CommandActionResults
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = PayCardResult.Simulate(currentWorld);
            currentWorld = CommandActionResults.Aggregate(currentWorld, (current, result) => result.Simulate(current));
            return currentWorld;
        }
    }
}
