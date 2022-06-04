using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record FallFailedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] Event<MoveEntity.SucceedResult> ShiftMoveEvent,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents,
        [NotNull] MoveEntity.Result Failed
    ) : Result;
}
