using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record SucceedResult(
        [NotNull] RemoveComponentAction Action,
        [NotNull] RemoveComponentProcess Process
    ) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
