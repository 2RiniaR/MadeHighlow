using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerResult(
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCardResult> SupplyCardResults
    ) : Result
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = RegisterPlayerResult.Simulate(currentWorld);
            currentWorld = AddComponentResults.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            currentWorld = SupplyCardResults.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            return currentWorld;
        }
    }
}
