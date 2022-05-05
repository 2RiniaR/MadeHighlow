using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record SucceedRunOperationResult : RunOperationResult
    {
        /// <summary>
        ///     カードを支払った結果
        /// </summary>
        [NotNull]
        public PayCardResult PayCard { get; init; } = new SucceedPayCardResult();

        /// <summary>
        ///     コマンドによるアクションを実行した結果
        /// </summary>
        [NotNull]
        public Result Command { get; init; } = Empty;

        public override World Simulate(in World world)
        {
            var currentWorld = world;
            currentWorld = PayCard.Simulate(currentWorld);
            currentWorld = Command.Simulate(currentWorld);
            return currentWorld;
        }
    }
}