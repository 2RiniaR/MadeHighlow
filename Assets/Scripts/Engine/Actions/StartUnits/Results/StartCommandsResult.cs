using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record StartCommandsResult : Result
    {
        /// <summary>
        ///     実行した命令
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<RunCommandResult> Operations { get; init; } =
            ValueObjectList<RunCommandResult>.Empty;

        public override World Simulate(in World world)
        {
            return Operations.Aggregate(
                world,
                (currentWorld, operationResult) => operationResult.Simulate(currentWorld)
            );
        }
    }
}