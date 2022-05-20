using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
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
