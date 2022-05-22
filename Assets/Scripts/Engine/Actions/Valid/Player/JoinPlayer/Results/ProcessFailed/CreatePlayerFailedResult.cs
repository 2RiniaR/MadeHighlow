using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreatePlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record CreatePlayerFailedResult(
        [NotNull] JoinPlayerAction Action,
        [NotNull] CreatePlayerResult Failed
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
