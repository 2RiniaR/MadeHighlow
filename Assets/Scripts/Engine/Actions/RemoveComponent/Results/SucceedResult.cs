using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record SucceedResult([NotNull] Component Removed) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return Removed.DeleteFrom(world);
        }
    }
}
