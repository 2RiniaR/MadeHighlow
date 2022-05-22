using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityProcess([NotNull] Event<CreateEntity.SucceedResult> CreateEntityEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateEntityEvent);
    }
}
