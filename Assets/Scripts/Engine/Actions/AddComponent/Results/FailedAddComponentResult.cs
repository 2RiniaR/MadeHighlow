using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedAddComponentResult
        ([NotNull] Component Component, FailedAddComponentReason Reason) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
