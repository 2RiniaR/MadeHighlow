using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record MoveFailedResult(
        [NotNull] EntityID TargetID,
        [NotNull] Direction3D Direction,
        [NotNull] MoveEntityResult FailedResult
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
