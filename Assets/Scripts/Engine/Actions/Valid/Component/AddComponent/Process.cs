using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record Process([NotNull] Event<CreateComponent.SucceedResult> CreateComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateComponentEvent);
    }
}
