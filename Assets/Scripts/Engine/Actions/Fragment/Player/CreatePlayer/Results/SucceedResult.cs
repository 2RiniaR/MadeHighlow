using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record SucceedResult(
        [NotNull] CreatePlayerAction Action,
        [NotNull] CreatePlayerProcess Process
    ) : CreatePlayerResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
