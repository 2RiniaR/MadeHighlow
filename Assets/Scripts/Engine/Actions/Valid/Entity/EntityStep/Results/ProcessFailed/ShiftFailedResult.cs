using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record ShiftFailedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] MoveEntity.Result Failed
    ) : Result;
}
