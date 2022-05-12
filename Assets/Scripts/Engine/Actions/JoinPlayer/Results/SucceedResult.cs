using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult(
        [NotNull] RegisterPlayer.Results.SucceedResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCardResult> SupplyCardResults
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
