using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PositionEntity;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record PositionFailedResult(
        [NotNull] MoveEntityAction Action,
        [NotNull] PositionEntityResult Failed
    ) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
