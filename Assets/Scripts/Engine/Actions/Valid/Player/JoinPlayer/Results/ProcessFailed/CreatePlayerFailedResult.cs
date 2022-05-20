using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreatePlayer;

namespace RineaR.MadeHighlow.Actions.Valid.JoinPlayer
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
