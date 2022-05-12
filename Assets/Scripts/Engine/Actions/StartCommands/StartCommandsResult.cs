using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public record StartCommandsResult([NotNull] [ItemNotNull] ValueList<RunCommandResult> Results) : Result
    {
        public override World Simulate(World world)
        {
            return Results.Aggregate(world, (currentWorld, operationResult) => operationResult.Simulate(currentWorld));
        }
    }
}
