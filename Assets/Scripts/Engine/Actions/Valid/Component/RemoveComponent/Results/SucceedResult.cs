using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public record SucceedResult
        ([NotNull] RemoveComponentAction Action, [NotNull] Process Process) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
