using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record EntityTeleportProcess([NotNull] Event<PositionEntity.SucceedResult> PositionEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PositionEntityEvent);
    }
}
