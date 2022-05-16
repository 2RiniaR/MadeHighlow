using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record SucceedResult(
        [NotNull] Fragment.MoveEntity.SucceedResult MoveEntityResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyEffect>> Interrupts
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return MoveEntityResult.Simulate(world);
        }
    }
}
