using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateEntity
{
    public record SucceedResult([NotNull] CreateEntityAction Action, [NotNull] Process Process) : CreateEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
