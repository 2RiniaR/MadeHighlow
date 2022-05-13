using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayer.SucceedResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<SupplyCard.SucceedResult> SupplyCardResults,
        [NotNull] Player Generated
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(RegisterPlayerResult)
                .Then(AddComponentResults)
                .Then(SupplyCardResults)
                .Simulate(world);
        }
    }
}
