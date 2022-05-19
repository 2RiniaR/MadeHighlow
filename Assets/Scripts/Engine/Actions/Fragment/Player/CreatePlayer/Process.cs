using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.Fragment.CreatePlayer
{
    public record Process(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterPlayerResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterCardEvent).Then(CreateComponentEvents);
    }
}
