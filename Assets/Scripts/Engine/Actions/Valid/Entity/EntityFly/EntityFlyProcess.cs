using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyProcess(
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FollowMoveEvents,
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(FollowMoveEvents).Then(FallMoveEvents);
    }
}
