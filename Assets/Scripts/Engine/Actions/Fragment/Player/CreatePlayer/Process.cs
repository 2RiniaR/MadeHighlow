using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record Process(
        [NotNull] Event<AllocateID.Result> AllocateIDEvent,
        [NotNull] Event<RegisterPlayer.Result> RegisterPlayerEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterPlayerEvent).Then(CreateComponentEvents);
    }
}
