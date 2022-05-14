using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record RejectedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] ActionFragments.RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] ActionFragments.PutCard.SucceedResult PutCardResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
