using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionEntity
{
    public record FailedResult([NotNull] PositionEntityAction Action, FailedReason Reason) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
