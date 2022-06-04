using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record Process([NotNull] Event<PositionEntity.SucceedResult> PositionEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PositionEntityEvent);
    }
}
