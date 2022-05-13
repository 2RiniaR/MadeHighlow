using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record SucceedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntity.SucceedResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionEntity.SucceedResult PositionEntityResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts,
        [NotNull] Entity Generated
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = RegisterEntityResult.Simulate(currentWorld);
            currentWorld = AddComponentResults.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            currentWorld = PositionEntityResult.Simulate(currentWorld);
            return currentWorld;
        }
    }
}
