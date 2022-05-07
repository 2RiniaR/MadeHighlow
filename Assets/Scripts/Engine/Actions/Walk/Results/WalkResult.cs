using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上を歩いて移動するアクションの結果
    /// </summary>
    public record WalkResult(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueObjectList<StepResult> StepResults
    ) : Result
    {
        public override World Simulate(World world)
        {
            return StepResults.Aggregate(world, (currentWorld, step) => step.Simulate(currentWorld));
        }
    }
}
