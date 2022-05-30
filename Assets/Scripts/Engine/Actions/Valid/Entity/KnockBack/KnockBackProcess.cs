using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBackProcess(
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> ShiftMoveEvents,
        [NotNull] ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents
    );
}
