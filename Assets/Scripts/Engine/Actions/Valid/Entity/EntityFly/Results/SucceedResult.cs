using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record SucceedResult(
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyEffect>> Interrupts,
        [NotNull] Fragment.MoveEntity.SucceedResult MoveEntityResult
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return MoveEntityResult.Simulate(world);
        }
    }
}
