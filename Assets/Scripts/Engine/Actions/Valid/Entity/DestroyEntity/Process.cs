using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record Process([NotNull] Event<DeleteEntity.SucceedResult> DeleteEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteEntityEvent);
    }
}
