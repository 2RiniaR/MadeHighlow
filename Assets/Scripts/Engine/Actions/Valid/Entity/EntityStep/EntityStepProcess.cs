using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record EntityStepProcess(
        [NotNull] ValueList<Event<Fragment.MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] Event<Fragment.MoveEntity.SucceedResult> ShiftMoveEvent,
        [NotNull] ValueList<Event<Fragment.MoveEntity.SucceedResult>> FallMoveEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(ClimbMoveEvents).Then(ShiftMoveEvent).Then(FallMoveEvents);
    }
}
