using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record Process(
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] Event<MoveEntity.SucceedResult> ShiftMoveEvent,
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(ClimbMoveEvents).Then(ShiftMoveEvent).Then(FallMoveEvents);
    }
}
