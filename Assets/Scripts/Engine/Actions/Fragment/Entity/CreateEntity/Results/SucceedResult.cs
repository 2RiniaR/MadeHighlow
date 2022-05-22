using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record SucceedResult(
        [NotNull] CreateEntityAction Action,
        [NotNull] CreateEntityProcess Process
    ) : CreateEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
