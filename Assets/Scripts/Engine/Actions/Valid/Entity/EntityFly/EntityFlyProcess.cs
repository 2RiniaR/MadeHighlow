using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyProcess([NotNull] Event<MoveEntity.SucceedResult> MoveEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(MoveEntityEvent);
    }
}
