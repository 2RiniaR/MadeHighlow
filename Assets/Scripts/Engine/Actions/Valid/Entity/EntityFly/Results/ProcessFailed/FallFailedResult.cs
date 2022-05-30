using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record FallFailedResult(
        [NotNull] EntityFlyAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ShiftMoveEvents,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents,
        [NotNull] MoveEntityResult Failed
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
