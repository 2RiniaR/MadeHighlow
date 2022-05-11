using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedPositionEntityResult
        ([NotNull] Entity Entity, FailedPositionEntityReason Reason) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
