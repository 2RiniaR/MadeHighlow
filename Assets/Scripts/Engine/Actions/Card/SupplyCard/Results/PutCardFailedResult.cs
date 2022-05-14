using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.PutCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record PutCardFailedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] ActionFragments.RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] PutCardResult FailedResult
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
