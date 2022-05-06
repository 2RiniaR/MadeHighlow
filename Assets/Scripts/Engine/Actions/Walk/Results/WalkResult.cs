using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上を歩いて移動するアクションの結果
    /// </summary>
    public record WalkResult(
        [NotNull] in EntityID ActorEntityID,
        [NotNull] [ItemNotNull] in ValueObjectList<StepResult> StepResults
    ) : Result
    {
        public override World Simulate(in World world)
        {
            return StepResults.Aggregate(world, (currentWorld, step) => step.Simulate(currentWorld));
        }
    }
}