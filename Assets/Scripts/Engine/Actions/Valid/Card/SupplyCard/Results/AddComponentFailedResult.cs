using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record AddComponentFailedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] Fragment.RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<AddComponentResult> FailedResult
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
