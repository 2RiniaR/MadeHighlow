using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record RegisterFailedResult(
        [NotNull] Player InitialStatus,
        [NotNull] RegisterPlayerResult FailedResult
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
