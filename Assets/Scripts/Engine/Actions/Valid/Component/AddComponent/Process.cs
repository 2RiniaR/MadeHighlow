using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public record Process([NotNull] Event<Fragment.CreateComponent.SucceedResult> CreateComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateComponentEvent);
    }
}
