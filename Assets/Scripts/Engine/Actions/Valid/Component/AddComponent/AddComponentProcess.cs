using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record AddComponentProcess([NotNull] Event<CreateComponent.SucceedResult> CreateComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateComponentEvent);
    }
}
