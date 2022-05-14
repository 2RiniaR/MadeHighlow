using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterPlayer;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record AddComponentFailedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult RegisterPlayerResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<AddComponentResult> FailedResult
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
