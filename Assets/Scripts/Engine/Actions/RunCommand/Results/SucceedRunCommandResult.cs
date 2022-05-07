using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record SucceedRunCommandResult(
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