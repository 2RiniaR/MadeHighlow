using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record StartCommandsResult([NotNull] [ItemNotNull] ValueObjectList<RunCommandResult> Results) : Result
    {
        public override World Simulate(in World world)
        {
            return Results.Aggregate(
                world,
                (currentWorld, operationResult) => operationResult.Simulate(currentWorld)
            );
        }
    }
}