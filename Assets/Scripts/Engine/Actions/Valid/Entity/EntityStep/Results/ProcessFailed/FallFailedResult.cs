using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record FallFailedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<Fragment.MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] Event<Fragment.MoveEntity.SucceedResult> ShiftMoveEvent,
        [NotNull] [ItemNotNull] ValueList<Event<Fragment.MoveEntity.SucceedResult>> FallMoveEvents,
        [NotNull] MoveEntityResult Failed
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
