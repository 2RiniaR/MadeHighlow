using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity
{
    public record SucceedResult([NotNull] Entity Positioned) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
