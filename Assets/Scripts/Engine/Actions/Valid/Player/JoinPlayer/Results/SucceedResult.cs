using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.Valid.JoinPlayer
{
    public record SucceedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<SupplyCard.SucceedResult>> SupplyCardResults,
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
