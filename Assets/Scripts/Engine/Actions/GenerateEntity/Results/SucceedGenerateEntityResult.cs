using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedGenerateEntityResult(
        [NotNull] Entity InitialEntity,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults,
        [NotNull] Entity GeneratedEntity
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = RegisterEntityResult.Simulate(currentWorld);
            currentWorld = AddComponentResults.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            return currentWorld;
        }
    }
}
