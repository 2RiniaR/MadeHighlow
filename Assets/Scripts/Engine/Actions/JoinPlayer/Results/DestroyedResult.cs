using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record DestroyedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
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
