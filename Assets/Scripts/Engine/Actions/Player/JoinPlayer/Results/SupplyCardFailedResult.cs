using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterPlayer;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SupplyCardFailedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<SupplyCard.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<SupplyCardResult> FailedResult
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
