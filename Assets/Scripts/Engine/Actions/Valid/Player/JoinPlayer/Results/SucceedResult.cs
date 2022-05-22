using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult(
        [NotNull] JoinPlayerAction Action,
        [NotNull] JoinPlayerProcess Process
    ) : JoinPlayerResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
