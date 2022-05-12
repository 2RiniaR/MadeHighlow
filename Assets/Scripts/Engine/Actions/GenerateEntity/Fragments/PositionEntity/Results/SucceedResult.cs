using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity
{
    public record SucceedResult([NotNull] Entity PositionedEntity) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            return PositionedEntity.UpdateIn(world);
        }
    }
}
