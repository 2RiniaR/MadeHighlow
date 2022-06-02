using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record FallFailedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] Event<MoveEntity.SucceedResult> ShiftMoveEvent,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents,
        [NotNull] MoveEntityResult Failed
    ) : EntityStepResult;
}
