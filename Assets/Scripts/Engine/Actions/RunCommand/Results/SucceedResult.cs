using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record SucceedResult(
        [NotNull] PayCardResult PayCardResult,
        [NotNull] Result CommandActionResult
    ) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = PayCardResult.Simulate(currentWorld);
            currentWorld = CommandActionResult.Simulate(currentWorld);
            return currentWorld;
        }
    }
}
