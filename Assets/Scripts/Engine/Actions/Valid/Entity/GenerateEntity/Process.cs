using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record Process([NotNull] [ItemNotNull] Event<Fragment.CreateEntity.SucceedResult> CreateEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateEntityEvent);
    }
}
