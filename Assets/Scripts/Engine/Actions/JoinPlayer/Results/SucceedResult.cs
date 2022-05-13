using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
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
