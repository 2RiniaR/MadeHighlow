using JetBrains.Annotations;

namespace RineaR.MadeHighlow.ActionFragments.PositionEntity
{
    public record FailedResult([NotNull] EntityID EntityID, FailedReason Reason) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
