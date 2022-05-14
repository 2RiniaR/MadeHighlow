using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record DestroyedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PutCard.SucceedResult PutCardResult
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
