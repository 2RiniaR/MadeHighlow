using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionEntity
{
    public record SucceedResult
        ([NotNull] PositionEntityAction Action, [NotNull] Entity Positioned) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
