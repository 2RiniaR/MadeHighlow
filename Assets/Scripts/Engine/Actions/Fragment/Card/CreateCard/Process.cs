using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record Process(
        [NotNull] Event<AllocateID.Result> AllocateIDEvent,
        [NotNull] Event<RegisterCard.SucceedResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterCardEvent).Then(CreateComponentEvents);
    }
}
