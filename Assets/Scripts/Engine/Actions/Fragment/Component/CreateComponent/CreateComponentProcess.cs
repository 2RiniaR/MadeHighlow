using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record CreateComponentProcess(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterComponent.SucceedResult> RegisterComponentEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(AllocateIDEvent).Then(RegisterComponentEvent);
    }
}
