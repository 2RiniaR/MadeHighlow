using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public record SucceedResult([NotNull] AddComponentAction Action, [NotNull] Process Process) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
