using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record SucceedRunCommandResult(
        [NotNull] in PayCardResult PayCardResult,
        [NotNull] in Result CommandActionResult
    ) : RunCommandResult
    {
        public override World Simulate(in World world)
        {
            var currentWorld = world;
            currentWorld = PayCardResult.Simulate(currentWorld);
            currentWorld = CommandActionResult.Simulate(currentWorld);
            return currentWorld;
        }
    }
}