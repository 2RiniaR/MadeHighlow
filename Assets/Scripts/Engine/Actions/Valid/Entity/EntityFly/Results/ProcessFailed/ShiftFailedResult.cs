using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record ShiftFailedResult(
        [NotNull] EntityFlyAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ShiftMoveEvents,
        [NotNull] MoveEntityResult Failed
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
