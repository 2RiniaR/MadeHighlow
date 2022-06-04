using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record Process(
        [NotNull] Event<AllocateID.Result> AllocateIDEvent,
        [NotNull] Event<RegisterComponent.SucceedResult> RegisterComponentEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(AllocateIDEvent).Then(RegisterComponentEvent);
    }
}
