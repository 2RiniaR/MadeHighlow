using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record Process(
        [NotNull] Event<AllocateID.Result> AllocateIDEvent,
        [NotNull] Event<RegisterEntity.Result> RegisterEntityEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterEntityEvent).Then(CreateComponentEvents);
    }
}
