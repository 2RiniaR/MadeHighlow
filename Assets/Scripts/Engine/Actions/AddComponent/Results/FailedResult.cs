using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record FailedResult([NotNull] Component Component, FailedReason Reason) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
