using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PutCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record PutCardFailedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] Fragment.RegisterCard.SucceedResult RegisterCardResult,
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
