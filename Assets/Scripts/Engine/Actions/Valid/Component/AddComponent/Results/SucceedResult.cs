using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult(
        [NotNull] AddComponentAction Action,
        [NotNull] AddComponentProcess Process
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
