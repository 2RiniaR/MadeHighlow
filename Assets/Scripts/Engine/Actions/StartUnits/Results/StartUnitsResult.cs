using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record StartUnitsResult : Result
    {
        /// <summary>
        ///     実行した命令
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<RunOperationResult> Operations { get; init; } =
            ValueObjectList<RunOperationResult>.Empty;

        public override World Simulate(in World world)
        {
            return Operations.Aggregate(
                world,
                (currentWorld, operationResult) => operationResult.Simulate(currentWorld)
            );
        }
    }
}