using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record Process(
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FollowMoveEvents,
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(FollowMoveEvents).Then(FallMoveEvents);
    }
}
