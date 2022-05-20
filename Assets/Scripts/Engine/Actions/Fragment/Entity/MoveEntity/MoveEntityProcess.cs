using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record MoveEntityProcess([NotNull] Event<PositionEntity.SucceedResult> PositionEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PositionEntityEvent);
    }
}
