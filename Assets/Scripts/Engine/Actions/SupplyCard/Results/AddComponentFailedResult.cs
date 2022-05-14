using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record AddComponentFailedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> SucceedResults,
        [NotNull] AddComponentResult FailedResult
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
