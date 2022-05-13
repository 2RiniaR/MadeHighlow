using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record DestroyedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayer.SucceedResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCard.SucceedResult> SupplyCardResults
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
