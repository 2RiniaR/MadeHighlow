using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record AddComponentFailedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayer.SucceedResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> SucceedResults,
        [NotNull] AddComponentResult FailedResult
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
