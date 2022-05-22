using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record CreatePlayerProcess(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterPlayerResult> RegisterPlayerEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterPlayerEvent).Then(CreateComponentEvents);
    }
}
