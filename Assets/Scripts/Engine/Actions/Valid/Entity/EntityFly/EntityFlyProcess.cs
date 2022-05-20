using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record EntityFlyProcess([NotNull] Event<Fragment.MoveEntity.SucceedResult> MoveEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(MoveEntityEvent);
    }
}
