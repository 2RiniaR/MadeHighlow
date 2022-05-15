using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record PositionFailedResult(
        [NotNull] EntityID TargetID,
        [NotNull] Direction3D Direction,
        [NotNull] PositionEntityResult FailedResult
    ) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
