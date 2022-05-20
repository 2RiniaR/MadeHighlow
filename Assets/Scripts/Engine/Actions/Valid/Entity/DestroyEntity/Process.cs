using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record Process([NotNull] [ItemNotNull] Event<Fragment.DeleteEntity.SucceedResult> DeleteEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteEntityEvent);
    }
}
