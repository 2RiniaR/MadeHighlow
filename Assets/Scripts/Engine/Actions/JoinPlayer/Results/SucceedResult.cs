using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayer.SucceedResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCard.SucceedResult> SupplyCardResults,
        [NotNull] Player Generated
    ) : JoinPlayerResult
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
