using JetBrains.Annotations;

namespace RineaR.MadeHighlow.ActionFragments.PositionEntity
{
    public record SucceedResult([NotNull] Entity Positioned) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
