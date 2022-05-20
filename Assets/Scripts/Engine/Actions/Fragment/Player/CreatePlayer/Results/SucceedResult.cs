using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreatePlayer
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
