using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record SucceedResult([NotNull] PositionEntity.SucceedResult PositionEntityResult) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return PositionEntityResult.Simulate(world);
        }
    }
}
