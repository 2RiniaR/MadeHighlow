using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record GenerateEntityProcess([NotNull] Event<Fragment.CreateEntity.SucceedResult> CreateEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateEntityEvent);
    }
}
