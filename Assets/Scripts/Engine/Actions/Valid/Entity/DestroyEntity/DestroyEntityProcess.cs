using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityProcess([NotNull] Event<DeleteEntity.SucceedResult> DeleteEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteEntityEvent);
    }
}
