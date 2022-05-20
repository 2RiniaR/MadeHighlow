using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record EntityTeleportProcess([NotNull] Event<Fragment.PositionEntity.SucceedResult> PositionEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PositionEntityEvent);
    }
}
