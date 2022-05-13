using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SupplyCardFailedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCard.SucceedResult> SucceedResults,
        [NotNull] SupplyCardResult FailedResult
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
