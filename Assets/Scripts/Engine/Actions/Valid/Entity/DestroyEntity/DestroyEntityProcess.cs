using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record DestroyEntityProcess([NotNull] Event<Fragment.DeleteEntity.SucceedResult> DeleteEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteEntityEvent);
    }
}
