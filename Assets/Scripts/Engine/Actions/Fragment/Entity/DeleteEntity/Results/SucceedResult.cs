using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteEntity
{
    public record SucceedResult(
        [NotNull] DeleteEntityAction Action,
        [NotNull] DeleteEntityProcess Process
    ) : DeleteEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
