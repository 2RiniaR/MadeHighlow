using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record CreateCardProcess(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterCard.SucceedResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterCardEvent).Then(CreateComponentEvents);
    }
}
