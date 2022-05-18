using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public record Process(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterComponent.SucceedResult> RegisterComponentEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(AllocateIDEvent).Then(RegisterComponentEvent);
    }
}
