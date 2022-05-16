using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record RejectedResult(
        [NotNull] Fragment.MoveEntity.SucceedResult MoveEntityResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
